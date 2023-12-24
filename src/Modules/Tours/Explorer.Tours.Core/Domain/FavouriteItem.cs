using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class FavouriteItem : Entity
    {
        public int ItemId { get; init; }
        public string ItemName { get; init; }
        public double Price { get; init; }
        public long WishlistId { get; init; }
      
     

        public FavouriteItem(int itemId, string itemName, double price, long wishlistId)
        {
            ItemId = itemId;
            ItemName = itemName;
            Price = price;
            WishlistId = wishlistId;
            
        }
    }
}
