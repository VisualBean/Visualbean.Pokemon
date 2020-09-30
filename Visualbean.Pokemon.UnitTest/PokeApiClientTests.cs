using LazyCache;
using LazyCache.Providers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Visualbean.Pokemon.Pokemon;

namespace Visualbean.Pokemon.UnitTest
{
    [TestClass]
    public class PokeApiClientTests
    {
        private const string PokemonName = "Test";
        private readonly string pokemonJson = @"{  
                'name': 'Test', 
                'flavor_text_entries': [{
                    'flavor_text': 'Some Text', 
                    'language': {
                        'name': 'en'
                    }
                }]
            }  
            ";

        [TestMethod]
        public async Task GetPokemon_WithCorrectName_ReturnsResult()
        {
            (PokeApiClient client, _) = SetupPokeApiClient();

            var result = await client.GetByNameAsync(PokemonName);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Value.Name == PokemonName);
        }

        [TestMethod]
        public async Task GetPokemon_WithNotFoundResponse_ReturnsFailureResult()
        {
            (PokeApiClient client, _) = SetupPokeApiClient(HttpStatusCode.NotFound);

            var result = await client.GetByNameAsync("SomeRandomNonExistentPokemonName");

            Assert.IsTrue(result.IsFailure, "Not Found from the API should result in a Failure.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetPokemon_WithAPIProblem_Throws()
        {
            (PokeApiClient client, _) = SetupPokeApiClient(HttpStatusCode.BadGateway);
            
            await client.GetByNameAsync(PokemonName);
        }

        [TestMethod]
        public async Task GetPokemon_WithSameName_ReturnsCachedResult()
        {
            (PokeApiClient client, MockHttpMessageHandler handler) = SetupPokeApiClient();
            
            var first = await client.GetByNameAsync(PokemonName);
            var second = await client.GetByNameAsync(PokemonName);

            Assert.AreEqual(first.Value, second.Value, "Both objects should be the same.");
            Assert.IsTrue(handler.NumberOfCalls == 1, "Second result should be cached.");
        }

        private (PokeApiClient client, MockHttpMessageHandler handler) SetupPokeApiClient(HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var handler = new MockHttpMessageHandler(pokemonJson, responseStatusCode);
            var cacheProvider = new Lazy<ICacheProvider>(() =>
               new MemoryCacheProvider(
                   new MemoryCache(
                       new MemoryCacheOptions())
               ));
            var client = new PokeApiClient(new HttpClient(handler),  new CachingService(cacheProvider));
            return (client, handler);
        }
    }
}
