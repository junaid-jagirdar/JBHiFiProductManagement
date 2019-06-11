using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using JBHiFi.ProductManagement.Business.Interfaces;
using JBHiFi.ProductManagement.Business.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class GetAllProducts : IQueryAll<Product>
    {
        public Result<IEnumerable<Product>> GetAll()
        {
            using (var db = new LiteDatabase(@"Products.db"))
            {

                try
                {
                    var products = db.GetCollection<Product>("products").FindAll();

                    return Result.Ok(products);
                }
                catch (Exception ex)
                {
                  return  Result.Fail<IEnumerable<Product>>(ex.Message);
                }
            }
        }

    }
}



