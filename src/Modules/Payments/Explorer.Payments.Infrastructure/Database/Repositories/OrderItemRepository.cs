using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly PaymentsContext _dbContext;
        public OrderItemRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderItem> GetOrderItemsByShoppingCart(int shoppingCartId)
        {
            return _dbContext.OrderItems.Where(o => o.ShoppingCartId == shoppingCartId).ToList();
        }

        public void RemoveAllItemsByShoppingCartId(int shoppingCartId)
        {
            var allItems = GetOrderItemsByShoppingCart(shoppingCartId);
            foreach (OrderItem item in allItems)
            {
                _dbContext.OrderItems.Remove(item);
                _dbContext.SaveChanges();
            }

        }
    }
}
