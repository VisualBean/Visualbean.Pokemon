// <copyright file="ShakespeareanPokemon.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Services
{
    using Newtonsoft.Json;

    /// <summary>
    /// A Shakespearean pokemon.
    /// </summary>
    public class ShakespeareanPokemon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShakespeareanPokemon"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public ShakespeareanPokemon(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

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
        [JsonProperty]
        public string Description { get; private set; }
    }
}