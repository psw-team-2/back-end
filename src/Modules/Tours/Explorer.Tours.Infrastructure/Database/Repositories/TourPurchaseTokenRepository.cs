using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourPurchaseTokenRepository: ITourPurchaseTokenRepository
    {
        private readonly ToursContext _dbContext;
        public TourPurchaseTokenRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TourPurchaseToken> GetByTour(int tourId)
        {
            return _dbContext.TourPurchaseToken
                .Where(token => token.TourId == tourId)
                .ToList();
        }

        public List<TourPurchaseToken> GetWeeklyByTour(int tourId)
        {
            DateTime lastWeekDate = DateTime.UtcNow.Date.AddDays(-7); // Get UTC date of a week ago

            return _dbContext.TourPurchaseToken
                .Where(token => token.TourId == tourId && token.PurchaseDate >= lastWeekDate)
                .ToList();
        }

    }
}
