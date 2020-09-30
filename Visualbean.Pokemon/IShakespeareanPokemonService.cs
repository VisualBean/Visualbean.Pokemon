// <copyright file="IShakespeareanPokemonService.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using System.Threading.Tasks;

    /// <summary>
    /// The IShakespeareanPokemonService interface.
    /// </summary>
    public interface IShakespeareanPokemonService
    {
        /// <summary>
        /// Gets the shakespearean pokemon asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A Result.</returns>
        Task<Result<ShakespeareanPokemon>> GetShakespeareanPokemonAsync(string name);
    }
}