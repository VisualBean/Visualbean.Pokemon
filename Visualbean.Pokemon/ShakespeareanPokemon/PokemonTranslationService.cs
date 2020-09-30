// <copyright file="PokemonTranslationService.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LazyCache;
    using Visualbean.Pokemon.Pokemon;
    using Visualbean.Pokemon.Shakespeare;

    public class PokemonTranslationService
    {
        private readonly IPokeApiClient pokeClient;
        private readonly ITranslationClient translationClient;
        private readonly IAppCache cache;

        public PokemonTranslationService(
            IPokeApiClient pokeClient,
            ITranslationClient translationClient,
            IAppCache cache)
        {
            this.pokeClient = pokeClient;
            this.translationClient = translationClient;
            this.cache = cache;
        }

        private static Result<ShakespeareanPokemon> NameMustBeSuppliedResult => Result.Fail<ShakespeareanPokemon>("Name must be supplied");

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
                    return Result.Fail<ShakespeareanPokemon>("No english description found.");
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
