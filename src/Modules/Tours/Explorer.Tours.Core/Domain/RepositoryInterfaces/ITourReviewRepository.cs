using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository 
    {
        IEnumerable<TourReview> GetReviewsForTour(int tourId);
        TourReview Create(TourReview tourReview);
        List<TourReview> GetByTourId(int tourId);
        TourPurchaseToken GetPurchaseToken(int tourId, int userId);
    }
}
