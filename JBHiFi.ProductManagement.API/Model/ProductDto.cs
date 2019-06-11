﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JBHiFi.ProductManagement.API.Model
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }
        
        public string Brand { get; set; }

    }
}
