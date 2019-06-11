using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.Interfaces
{
    public interface IQueryHandler
    {
        /// <summary>
        /// Handles the specified input.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        TOutput Handle<TInput, TOutput>(TInput input);

        /// <summary>
        /// Handles the specified connection string.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Result<IEnumerable<TOutput>> Handle<TOutput>();
    }
}
