// <copyright file="ShakespeareSuccess.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Shakespeare
{
    using Newtonsoft.Json;

    /// <summary>
    /// The ShakespeareSuccess.
    /// </summary>
    public class ShakespeareSuccess
    {
        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty]
        public int Total { get; private set; }
    }
}