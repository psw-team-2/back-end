using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ShoppingCart : Entity
    {
        public long UserId { get; init; }
        public List<OrderItem>? Items { get; init; }
        public double TotalPrice { get; init; }

        public ShoppingCart(long userId, double totalPrice)
        {
            UserId = userId;
            Items = new List<OrderItem>();
            TotalPrice = totalPrice;
        }
    }
}
