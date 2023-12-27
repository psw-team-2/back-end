using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourPurchaseTokenRepository
    {
        public List<TourPurchaseToken> GetByTour(int tourId);
        public List<TourPurchaseToken> GetWeeklyByTour(int tourId);
    }
}
