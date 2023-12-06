using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ComposedTourRepository: IComposedTourRepository
    {
        private readonly ToursContext _dbContext;
        public ComposedTourRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
