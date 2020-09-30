// <copyright file="Language.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Pokemon
{
    /// <summary>
    /// The Language.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Language(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        public static implicit operator string(Language language) => language.Name;

        public static implicit operator Language(string name) => new Language(name);
    }
}
