using LazyCache;
using LazyCache.Providers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Visualbean.Pokemon.Shakespeare;

namespace Visualbean.Pokemon.UnitTest
{
    [TestClass]
    public class ShakespeareTranslationClientTests
    {
        private const string Text = "SomeText";
        private readonly string jsonResult = @"{
          'success': {
            'total': 1
          },
          'contents': {
            'translated': 'Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.',
            'text': 'You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.',
            'translation': 'shakespeare'
          }
        }";

        [TestMethod]
        public async Task GetTranslation_WithValidInput_ReturnsResult()
        {
            (ShakespeareTranslationClient client, _) = SetupTranslationClientClient();

            var result = await client.GetTranslationAsync(Text);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value), "results should have a value.");
        }

        [TestMethod]
        public async Task GetTranslation_WithTooManyRequests_ReturnsFailureResult()
        {
            (ShakespeareTranslationClient client, _) = SetupTranslationClientClient(HttpStatusCode.TooManyRequests);

            var result = await client.GetTranslationAsync(Text);

            Assert.IsTrue(result.IsFailure, "Too many requests from the API should result in a Failure.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetTranslation_WithAPIProblem_Throws()
        {
            (ShakespeareTranslationClient client, _) = SetupTranslationClientClient(HttpStatusCode.BadGateway);

            await client.GetTranslationAsync(Text);
        }

        [TestMethod]
        public async Task GetTranslation_WithSameInput_ReturnsCachedResult()
        {
            (ShakespeareTranslationClient client, MockHttpMessageHandler handler) = SetupTranslationClientClient();

            var first = await client.GetTranslationAsync(Text);
            var second = await client.GetTranslationAsync(Text);

            Assert.AreEqual(first.Value, second.Value, "Both results should be the same.");
            Assert.IsTrue(handler.NumberOfCalls == 1, "Second result should be cached.");
        }

        private (ShakespeareTranslationClient client, MockHttpMessageHandler handler) SetupTranslationClientClient(HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var handler = new MockHttpMessageHandler(jsonResult, responseStatusCode);
            var cacheProvider = new Lazy<ICacheProvider>(() =>
               new MemoryCacheProvider(
                   new MemoryCache(
                       new MemoryCacheOptions())
               ));
            var translationClient = new ShakespeareTranslationClient(new HttpClient(handler), new CachingService(cacheProvider));
            return (translationClient, handler);
        }
    }
}
