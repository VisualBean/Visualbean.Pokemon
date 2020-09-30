// <copyright file="PokemonController.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The pokemon controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IShakespeareanPokemonService shakespeareanPokemonService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonController" /> class.
        /// </summary>
        /// <param name="shakespeareanPokemonService">The shakespearean pokemon service.</param>
        public PokemonController(IShakespeareanPokemonService shakespeareanPokemonService)
        {
            this.shakespeareanPokemonService = shakespeareanPokemonService;
        }

        /// <summary>
        /// Gets the pokemon.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A shakespearean pokemon.</returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon([FromRoute]string name)
        {
            name = name?.Trim().ToLower();
            if (string.IsNullOrEmpty(name))
            {
                return this.BadRequest("Please supply a valid name.");
            }

            var result = await this.shakespeareanPokemonService.GetShakespeareanPokemonAsync(name);
            switch (result.Status)
            {
                case Status.Ok:
                    return this.Ok(result.Value);
                case Status.NotFound:
                    return this.NotFound(result.Error);
                case Status.Error:
                    return this.BadRequest(result.Error);
            }

            return this.StatusCode(500);
        }
    }
}
