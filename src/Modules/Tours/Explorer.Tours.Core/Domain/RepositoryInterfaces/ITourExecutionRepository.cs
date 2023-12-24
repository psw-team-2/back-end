using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionRepository
    {
        TourExecution Create(TourExecution entity);
        TourExecution Get(long id);
        TourExecution Update(TourExecution entity);

        TourExecution GetTourExecutionForTourist(int tourId, int touristId);

        public List<TourExecution> GetExecutedToursByTourAndUserId(int tourId, int userId);
        public List<TourExecution> GetActiveExecutedToursByTourIds(List<long> tourIds);
    }
}
