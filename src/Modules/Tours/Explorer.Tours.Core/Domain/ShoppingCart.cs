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
        public List<int>? Items { get; set; }
        public Price TotalPrice { get; set; }

        public ShoppingCart(long userId, Price totalPrice)
        {
            UserId = userId;
            Items = new List<int>();
            TotalPrice = totalPrice;
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
            TotalPrice = new Price();
        }


        public void CalculateTotalPrice(Price totalPrice, Price itemPrice, bool isAdding)
        {
            if (Items != null)
            {
                if (isAdding)
                {
                    totalPrice = new Price(totalPrice.Amount + itemPrice.Amount);
                }
                else
                {
                    totalPrice = new Price(totalPrice.Amount - itemPrice.Amount);
                }
                
                TotalPrice = totalPrice; 
            }
        }

    }
}
