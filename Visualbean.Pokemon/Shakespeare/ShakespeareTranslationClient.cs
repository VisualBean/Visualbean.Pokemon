// <copyright file="SharespeareTranslationClient.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Shakespeare
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using LazyCache;
    using Newtonsoft.Json;

    /// <summary>
    /// The translationClient.
    /// </summary>
    public class ShakespeareTranslationClient : ITranslationClient
    {
        private readonly HttpClient httpClient;

        private readonly IAppCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShakespeareTranslationClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="cache">The cache.</param>
        public ShakespeareTranslationClient(HttpClient httpClient, IAppCache cache)
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
            }

            this.httpClient = httpClient;
            this.cache = cache;
        }

        private static Result<string> NoTextSuppliedResult => Result.Fail<string>("Text to translate, must be supplied.");

        private static Result<string> TooManyRequestsResult => Result.Fail<string>("Too many requests.");

        /// <inheritdoc/>
        public async Task<Result<string>> GetTranslationAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return NoTextSuppliedResult;
            }

            var cacheKey = text.ToLower().GetHashCode().ToString();
            var translatedText = this.cache.Get<string>(cacheKey);

            if (translatedText == null)
            {
                var body = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("text", text),
                });

                var response = await this.httpClient.PostAsync("shakespeare.json/", body);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    translatedText = JsonConvert.DeserializeObject<ShakespeareResult>(content).Contents.TranslatedText;

                    this.cache.Add(cacheKey, translatedText);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.TooManyRequests)
                    {
                        return TooManyRequestsResult;
                    }

                    throw new Exception("Something went wrong.");
                }
            }

            return Result.Ok<string>(translatedText);
        }
    }
}
