// <copyright file="Pokemon.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// The pokemon.
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty("flavor_text_entries")]
        public IEnumerable<FlavourEntry> FlavourEntries { get; private set; }
    }
}
