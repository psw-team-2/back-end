using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        Tour GetOne(int tourId);
        Tour Update(Tour tour);
        List<TourReview> GetByTourId(int tourId);
        List<Tour> GetToursByAuthorId(int authorId);
        Tour Get(long id);
    }
}
