using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business.Entities;
using CSharpFunctionalExtensions;
using LiteDB;

namespace JBHiFi.ProductManagement.Business
{
    public class GetProductById : IQueryFor<string, Result<Product>>
    {
        public Result<Product> Execute(string id)
        {
            using (var db = new LiteDatabase(@"Products.db"))
            {

                try
                {
                    var product = db.GetCollection<Product>("products").FindOne(x => x.Id == id);

                    return Result.Ok(product);
                }
                catch (Exception ex)
                {
                    return Result.Fail<Product>(ex.Message);
                }
            }
        }
    }
}
