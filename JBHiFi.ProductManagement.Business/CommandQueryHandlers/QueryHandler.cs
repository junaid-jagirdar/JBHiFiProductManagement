using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.Business.Interfaces;
using StructureMap;
using System.Collections.Generic;

namespace JBHiFi.ProductManagement.Business.CommandQueryHandlers
{
    public class QueryHandler : IQueryHandler
    {
        /// <summary>
        /// The container
        /// </summary>
        private readonly IContainer _container;

        
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="log">The log.</param>
        public QueryHandler(IContainer container)
        {
            _container = container;
           
        }
        /// <summary>
        /// Handles the specified input.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="input">The input.</param>
        public TOutput Handle<TInput, TOutput>(TInput input)
        {
            var queryHandlerInstance = _container.GetInstance<IQueryFor<TInput, TOutput>>();
           
                return queryHandlerInstance.Execute(input);
            
        }

        /// <summary>
        /// Handles this instance.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <returns></returns>
        public Result<IEnumerable<TOutput>> Handle<TOutput>()
        {
            var queryHandlerInstance = _container.GetInstance<IQueryAll<TOutput>>();
          
                return queryHandlerInstance.GetAll();
            
        }
    }
}
