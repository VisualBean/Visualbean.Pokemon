// <copyright file="ShakespeareContents.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Shakespeare
{
    using Newtonsoft.Json;

    /// <summary>
    /// The ShakespeareContents.
    /// </summary>
    public class ShakespeareContents
    {
        /// <summary>
        /// Gets the translated.
        /// </summary>
        /// <value>
        /// The translated.
        /// </value>
        [JsonProperty("translated")]
        public string TranslatedText { get; private set; }
    }
}