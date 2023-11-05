﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public double TotalPrice { get; set; }
    }
}

