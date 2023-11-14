using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class OrderItem : Entity
    {
        public int TourId { get; init; }
        public string TourName { get; init; }
        public double Price { get; init; }
        public long ShoppingCartId { get; init; }

        public OrderItem(int tourId, string tourName, double price, long shoppingCartId)
        {
            TourId = tourId;
            TourName = tourName;
            Price = price;
            ShoppingCartId = shoppingCartId;
        }
    }
}
