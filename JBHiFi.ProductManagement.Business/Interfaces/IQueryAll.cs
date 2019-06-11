using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.Interfaces
{
    public interface IQueryAll<T>
    {
        /// <summary>
        /// Get All the records for Particular Types
        /// </summary>

       /// <returns>
        /// IEnumerable of the Object
        /// </returns>
        Result<IEnumerable<T>> GetAll();
    }
}
