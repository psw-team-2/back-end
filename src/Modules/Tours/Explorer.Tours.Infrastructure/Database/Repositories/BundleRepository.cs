using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
﻿using Explorer.Payments.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

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
        /*
        public Bundle GetBundleByTourId(int bundleId)
        {
            var bundle = _dbContext.Bundles
                .Include(b => b.Tours) // Include the Tours related to the Bundle
                .FirstOrDefault(b => b.Id == bundleId);

            return bundle;
        }*/

        public void Delete(long id)
        {
            var entity = _dbContext.Bundles.Find(id);
            if (entity != null)
            {
                _dbContext.Bundles.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}
