// <copyright file="FlavourEntry.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Pokemon
{
    using Newtonsoft.Json;

    /// <summary>
    /// The FlavourEntry.
    /// </summary>
    public class FlavourEntry
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty("flavor_text")]
        public string Text { get; private set; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [JsonProperty]
        public Language Language { get; private set; }

        public static implicit operator string(FlavourEntry entry) => entry.Text;

        public static explicit operator FlavourEntry(string text) => new FlavourEntry { Text = text };
    }
}
