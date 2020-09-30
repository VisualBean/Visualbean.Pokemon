// <copyright file="PokeApiClient.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Pokemon
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using LazyCache;
    using Newtonsoft.Json;

    /// <summary>
    /// Simple client as the PokeApi.Net client was broken.
    /// </summary>
    /// <seealso cref="IPokeApiClient" />
    public class PokeApiClient : IPokeApiClient
    {
        private readonly HttpClient httpClient;

        private readonly IAppCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokeApiClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="cache">The cache.</param>
        public PokeApiClient(HttpClient httpClient, IAppCache cache)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("https://pokeapi.co");
            }

            this.httpClient = httpClient;
            this.cache = cache;
        }

        private static Result<Pokemon> PokemonNotFoundResult => Result.Fail<Pokemon>("Pokemon not found.");

        private static Result<Pokemon> NameMustBeSuppliedResult => Result.Fail<Pokemon>("Name must be supplied");

        /// <inheritdoc/>
        public async Task<Result<Pokemon>> GetByNameAsync(string name)
        {
            name = name?.Trim().ToLower();
            if (string.IsNullOrEmpty(name))
            {
                return NameMustBeSuppliedResult;
            }

            return await this.cache.GetOrAddAsync(name, async () =>
            {
                var response = await this.httpClient.GetAsync($"/api/v2/pokemon-species/{name}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(content);
                    return Result.Ok(pokemon);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return PokemonNotFoundResult;
                    }

                    throw new Exception("Something went wrong.");
                }
            });
        }
    }
}
