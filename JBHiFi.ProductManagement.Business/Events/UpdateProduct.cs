using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class UpdateProduct : ICommand<ProductForUpdate>
    {
        public void Execute(ProductForUpdate productToUpdate)
        {
            using (var db = new LiteDatabase(@"Products.db"))
            {
                var products = db.GetCollection<Product>("products");
                products.Update(new Product() { Id= productToUpdate.Id, Brand =productToUpdate.Brand, Description=productToUpdate.Description, Model =productToUpdate.Model});
            }
        }
    }
}
