using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.CommandQueryHandlers
{
    public interface IQueryFor<in TInput, out TOutput>
    {
        /// <summary>
        /// Executes the Query for the Input
        /// </summary>
        /// <param name="input">Input for the Query </param>
        /// <param name="connectionString"></param>
        /// <returns>Output for the query</returns>
        TOutput Execute(TInput input);
    }
}
