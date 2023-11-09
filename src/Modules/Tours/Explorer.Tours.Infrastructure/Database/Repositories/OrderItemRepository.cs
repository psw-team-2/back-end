using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ToursContext _dbContext;
        public OrderItemRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderItem GetAllByShoppingCartId(int shoppingCartId)
        {
            return _dbContext.OrderItems.FirstOrDefault(oi => oi.ShoppingCartId == shoppingCartId);
        }
    }
}
