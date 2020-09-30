// <copyright file="PokemonController.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The pokemon controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> logger;
        private readonly IPokeApiClient pokeClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonController"/> class.
        /// </summary>
        /// <param name="pokeClient">The Poke API Client.</param>
        /// <param name="logger">The logger.</param>
        public PokemonController(IPokeApiClient pokeClient, ILogger<PokemonController> logger)
        {
            this.logger = logger;
            this.pokeClient = pokeClient;
        }
    }
}
