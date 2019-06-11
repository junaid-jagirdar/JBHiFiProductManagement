using JBHiFi.ProductManagement.Business.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business.Entities
{

   public class ProductInfoContext: LiteDatabase
    {
        public ProductInfoContext()
            : base(@"Products.db")
        {

        }


    }
}
