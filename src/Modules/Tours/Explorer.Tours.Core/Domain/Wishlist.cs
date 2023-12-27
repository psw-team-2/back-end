using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Wishlist : Entity
    {
        public long UserId { get; init; }
        public List<int>? Items { get; set; }

        public Wishlist(long userId, List<int>? items)
        {
            UserId = userId;
            Items = items;
        }

        public void AddItem(int itemId)
        {
            if (Items != null)
            {
                Items.Add(itemId);
            }
        }

        public void RemoveItem(int itemId)
        {
            if (Items != null)
            {
                Items.Remove(itemId);
            }
        }

        public void RemoveAllItems()
        {
            Items = new List<int>();
            
        }
    }
}
