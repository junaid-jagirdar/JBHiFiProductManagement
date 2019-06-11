using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class AddProduct : ICommand<Product>
    {
        public void Execute(Product input)
        {
            using (var db = new LiteDatabase(@"Products.db"))
            {
                var products = db.GetCollection<Product>("products");
                products.Insert(input);
            }
        }
    }
}
