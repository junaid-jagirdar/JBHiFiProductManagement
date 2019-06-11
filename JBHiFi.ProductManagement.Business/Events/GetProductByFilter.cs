using CSharpFunctionalExtensions;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using JBHiFi.ProductManagement.Business.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class GetProductsByFilter : IQueryFor<ProductFilter, Result<IEnumerable<Product>>>
    {
        public Result<IEnumerable<Product>> Execute(ProductFilter productFilter)
        {
            using (var db = new LiteDatabase(@"Products.db"))
            {

                try
                {
                    var products = db.GetCollection<Product>("products").Find(x => (productFilter.Brand == x.Brand || productFilter.Brand == x.Description || productFilter.Model == x.Model));

                    return Result.Ok(products);
                }
                catch (Exception ex)
                {
                    return Result.Fail<IEnumerable<Product>>(ex.Message);
                }
            }
        }
    }
}
