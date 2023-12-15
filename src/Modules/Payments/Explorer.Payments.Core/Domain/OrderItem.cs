using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class OrderItem : Entity
    {
        public int ItemId { get; init; }
        public string ItemName { get; init; }
        public double Price { get; init; }
        public long ShoppingCartId { get; init; }
        public bool IsBought { get; init; }
        public bool IsBundle { get; init; }
        public string Image { get; init; }

        public OrderItem(int itemId, string itemName, double price, long shoppingCartId, bool isBought, bool isBundle, string image)
        {
            ItemId = itemId;
            ItemName = itemName;
            Price = price;
            ShoppingCartId = shoppingCartId;
            IsBought = isBought;
            IsBundle = isBundle;
            Image = image;
        }
    }
}
