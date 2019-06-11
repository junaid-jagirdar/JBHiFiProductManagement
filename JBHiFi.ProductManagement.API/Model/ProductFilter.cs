using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JBHiFi.ProductManagement.API.Model
{
    public class ProductFilterDto
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
    }
}
