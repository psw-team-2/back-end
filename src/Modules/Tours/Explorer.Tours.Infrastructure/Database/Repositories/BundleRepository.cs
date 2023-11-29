using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class BundleRepository : IBundleRepository
    {
        private readonly ToursContext _dbContext;
        public BundleRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Bundle> GetBundlesByAuthorId(int userId)
        {
            return _dbContext.Bundles.Where(c => c.UserId == userId).ToList();
        }
    }
}
