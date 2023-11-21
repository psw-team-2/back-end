using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PaymentsContext _dbContext;
        public ShoppingCartRepository(PaymentsContext dbContext)
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

        public double GetTotalPriceByUserId(int userId)
        {
            return _dbContext.ShoppingCarts.FirstOrDefault(sc => sc.Id == userId).TotalPrice;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            ShoppingCart oldShoppingCart = _dbContext.ShoppingCarts.FirstOrDefault(sc => sc.Id == shoppingCart.Id);
            _dbContext.Entry(oldShoppingCart).CurrentValues.SetValues(shoppingCart);
            _dbContext.SaveChanges();
        }

        

    }
}
