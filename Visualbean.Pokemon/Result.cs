// <copyright file="Result.cs" company="Visualbean">
// Copyright (c) Visualbean. All rights reserved.
// </copyright>

namespace Visualbean.Pokemon
{
    using System;

    /// <summary>
    /// A Result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="error">The error.</param>
        /// <exception cref="InvalidOperationException"></exception>
        protected Result(Status status, string error)
        {
            this.Status = status;
            this.Error = error;

            if ((this.IsSuccess && !string.IsNullOrWhiteSpace(error))
               || (!this.IsSuccess && string.IsNullOrWhiteSpace(error)))
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess => (int)this.Status < 10;

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; private set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is failure.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is failure; otherwise, <c>false</c>.
        /// </value>
        public bool IsFailure => !this.IsSuccess;

        /// <summary>
        /// Fails the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A result.</returns>
        public static Result Fail(string message) => new Result(Status.Error, message);

        /// <summary>
        /// Fails the specified message.
        /// </summary>
        /// <typeparam name="T">The type of data stored.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>A result.</returns>
        public static Result<T> Fail<T>(string message) => new Result<T>(default(T), Status.Error, message);

        /// <summary>
        /// Oks the specified value.
        /// </summary>
        /// <typeparam name="T">The type of data stored.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A result.</returns>
        public static Result<T> Ok<T>(T value) => new Result<T>(value, Status.Ok, error: null);
    }
}