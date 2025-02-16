﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    }
}
