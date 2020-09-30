// <copyright file="Result{T}.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using System;

    /// <summary>
    /// A Result with an associated Value.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public class Result<T> : Result
    {
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="error">The error.</param>
        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        /// <exception cref="InvalidOperationException"></exception>
        public T Value
        {
            get
            {
                if (!this.IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return this.value;
            }
        }
    }
}