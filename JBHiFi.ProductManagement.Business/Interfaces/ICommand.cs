using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.Interfaces
{
    public interface ICommand<TInput1>
    {
        /// <summary>
        /// Executes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        void Execute(TInput1 input);
    }
}
