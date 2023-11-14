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
        public double TotalPrice { get; set; }

        public ShoppingCart(long userId, double totalPrice)
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
            TotalPrice = 0;
        }


        public void CalculateTotalPrice(double totalPrice, double itemPrice, bool isAdding)
        {
            if (Items != null)
            {
                if (isAdding)
                {
                    totalPrice += itemPrice;
                }
                else
                {
                    totalPrice -= itemPrice;
                }
                
                TotalPrice = totalPrice; 
            }
        }

    }
}
