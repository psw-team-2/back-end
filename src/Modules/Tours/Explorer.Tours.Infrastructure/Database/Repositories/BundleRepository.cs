using Explorer.Payments.Core.Domain;
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
    public class BundleRepository : IBundleRepository
    {
        private readonly ToursContext _dbContext;

        public BundleRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Bundle GetById(int bundleId)
        {
            return _dbContext.Bundles.FirstOrDefault(b => b.Id == bundleId);
        }

        public void Update(Bundle bundle)
        {
            Bundle oldBundle = _dbContext.Bundles.FirstOrDefault(b => b.Id == bundle.Id);
            _dbContext.Entry(oldBundle).CurrentValues.SetValues(bundle);
            _dbContext.SaveChanges();
        }
    }
}