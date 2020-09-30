// <copyright file="IPokeApiClient.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using System.Threading.Tasks;

    /// <summary>
    /// The PokeApi client.
    /// </summary>
    public interface IPokeApiClient
    {
        /// <summary>
        /// Get pokemon by name.
        /// </summary>
        /// <param name="name">Name of the pokemon.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Result<Pokemon>> GetByNameAsync(string name);
    }
}