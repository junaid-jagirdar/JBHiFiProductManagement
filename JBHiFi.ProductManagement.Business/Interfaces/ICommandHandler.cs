using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.Interfaces
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Handles the specified input.
        /// </summary>
        /// <typeparam name="TInput1">The type of the input1.</typeparam>
         void Handle<TInput1>(TInput1 input);
    }
}
