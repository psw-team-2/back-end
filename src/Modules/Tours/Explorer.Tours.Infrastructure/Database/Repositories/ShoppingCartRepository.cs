using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ToursContext _dbContext;
        public ShoppingCartRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ShoppingCart GetById(int shoppingCartId)
        {          
            return _dbContext.ShoppingCarts.FirstOrDefault(sc => sc.Id == shoppingCartId);
        }

        public ShoppingCart GetShoppingCartByUserId(int userId)
        {
            return _dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == userId);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            ShoppingCart oldShoppingCart = _dbContext.ShoppingCarts.FirstOrDefault(sc => sc.Id == shoppingCart.Id);
            _dbContext.Entry(oldShoppingCart).CurrentValues.SetValues(shoppingCart);
            _dbContext.SaveChanges();
        }

        /*public List<OrderItem> GetAllItemsForShoppingCart(long shoppingCartId)
        {
            List<OrderItem> items = _dbContext.ShoppingCarts.Where(cart => cart.Id == shoppingCartId).SelectMany(cart => cart.Items).ToList();
            return items;
        }*/

        /*public void UpdateTotalPrice(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null) throw new ArgumentException("ShoppingCart is null.");

            List<OrderItem> items = GetAllItemsForShoppingCart(shoppingCart.Id);
            shoppingCart.TotalPrice = items?.Sum(item => item.Price) ?? 0;
            _dbContext.SaveChanges(); 
        }*/

    }
}
