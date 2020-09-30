// <copyright file="PokemonTranslationService.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using System.Linq;
    using System.Threading.Tasks;
    using LazyCache;
    using Visualbean.Pokemon.Pokemon;
    using Visualbean.Pokemon.Shakespeare;

    /// <summary>
    /// The ShakespeareanPokemonService.
    /// </summary>
    public class ShakespeareanPokemonService : IShakespeareanPokemonService
    {
        private readonly IPokeApiClient pokeClient;
        private readonly ITranslationClient translationClient;
        private readonly IAppCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShakespeareanPokemonService"/> class.
        /// </summary>
        /// <param name="pokeClient">The poke client.</param>
        /// <param name="translationClient">The translation client.</param>
        /// <param name="cache">The cache.</param>
        public ShakespeareanPokemonService(
            IPokeApiClient pokeClient,
            ITranslationClient translationClient,
            IAppCache cache)
        {
            this.pokeClient = pokeClient;
            this.translationClient = translationClient;
            this.cache = cache;
        }

        private static Result<ShakespeareanPokemon> NameMustBeSuppliedResult => Result.Fail<ShakespeareanPokemon>("Name must be supplied");

        /// <inheritdoc/>
        public async Task<Result<ShakespeareanPokemon>> GetShakespeareanPokemonAsync(string name)
        {
            name = name?.Trim().ToLower();

            if (string.IsNullOrEmpty(name))
            {
                return NameMustBeSuppliedResult;
            }

            return await this.cache.GetOrAddAsync(name, async () =>
            {
                var pokemonResult = await this.pokeClient.GetByNameAsync(name);
                if (pokemonResult.IsFailure)
                {
                    return Result.Fail<ShakespeareanPokemon>(pokemonResult.Error);
                }

                var textToTranslate = pokemonResult.Value.FlavourEntries.FirstOrDefault(entry => entry.Language == Language.English);
                if (string.IsNullOrEmpty(textToTranslate))
                {
                    return new Result<ShakespeareanPokemon>(default, Status.NotFound, "No english description found.");
                }

                var translationResult = await this.translationClient.GetTranslationAsync(textToTranslate);
                if (translationResult.IsFailure)
                {
                     return Result.Fail<ShakespeareanPokemon>(translationResult.Error);
                }

                return Result.Ok(new ShakespeareanPokemon(name, translationResult.Value));
            });
        }
    }
}
