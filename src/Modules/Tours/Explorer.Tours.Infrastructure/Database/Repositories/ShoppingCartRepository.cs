using Explorer.Stakeholders.Core.Domain;
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
    }
}
