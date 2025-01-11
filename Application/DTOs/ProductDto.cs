﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }

        public bool InStock { get; set; }

        public ProductDto()
        {
            if (StockQuantity == 0)
            {
                InStock = false;
            }
            InStock = true;
        }
    }
}
