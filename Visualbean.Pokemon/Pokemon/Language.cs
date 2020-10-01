// <copyright file="Language.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Pokemon
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The Language.
    /// </summary>
    public class Language : IEquatable<Language>
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
        /// Gets the english. // Too funny.. had to leave in..
        /// </summary>
        /// <value>
        /// The english.
        /// </value>
        public static Language English => "en";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        public static implicit operator string(Language language) => language.Name;

        public static implicit operator Language(string name) => new Language(name);

        public static bool operator ==(Language left, Language right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Language left, Language right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Language);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] Language other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return this.Name == other.Name;
        }
    }
}
