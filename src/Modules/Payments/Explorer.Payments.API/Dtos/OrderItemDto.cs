using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public double Price { get; set; }
        public long ShoppingCartId { get; set; }
        public bool IsBought { get; set; }
    }
}
