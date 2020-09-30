// <copyright file="ShakespeareResult.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon.Shakespeare
{
    using Newtonsoft.Json;

    /// <summary>
    /// The ShakespeareResult.
    /// </summary>
    public class ShakespeareResult
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        [JsonProperty]
        public ShakespeareSuccess Success { get; private set; }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        [JsonProperty]
        public ShakespeareContents Contents { get; private set; }
    }
}