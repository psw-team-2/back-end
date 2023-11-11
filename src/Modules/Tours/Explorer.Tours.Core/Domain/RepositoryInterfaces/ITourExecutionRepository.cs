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
    }
}
