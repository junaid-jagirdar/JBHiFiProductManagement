using JBHiFi.ProductManagement.Business.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.CommandQueryHandlers
{
    public class CommandHandler : ICommandHandler
    {
        /// <summary>
        /// The container
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandler" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
 
        public CommandHandler(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Handles the specified input.
        /// </summary>
        /// <typeparam name="TInput1">The type of the input1.</typeparam>
        public void Handle<TInput1>(TInput1 input)
        {
            var commandHandlerInstance = _container.GetInstance<ICommand<TInput1>>();
           
                commandHandlerInstance.Execute(input);
            
        }
    }
}
