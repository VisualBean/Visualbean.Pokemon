// <copyright file="ITranslationClient.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Shakespeare
{
    using System.Threading.Tasks;

    /// <summary>
    /// The ITranslationClient interface.
    /// </summary>
    public interface ITranslationClient
    {
        /// <summary>
        /// Gets the translation.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <returns>A result.</returns>
        Task<Result<string>> GetTranslation(string text);
    }
}