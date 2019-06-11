using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class DeleteProduct : ICommand<string>
    {
        public void Execute(string id)
        {

            using (var db = new LiteDatabase(@"Products.db"))
            {
                var products = db.GetCollection<Product>("products");
                products.Delete(x => x.Id == id);
            }
        }
    }
}
