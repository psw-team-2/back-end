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
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _context;

        public TourRepository(ToursContext context)
        {
            _context = context;
        }

        public Tour GetOne(int tourId)
        {
            return _context.Tour
        .Include(t => t.TourReviews)  // Ensure that TourReviews are included
        .FirstOrDefault(t => t.Id == tourId);

        }
        public Tour Update(Tour tour)
        {
            try
            {
                _context.Update(tour);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return (Tour)tour;
        }

        public List<TourReview> GetByTourId(int tourId)
        {
            return _context.TourReview
                .Where(tr => tr.TourId == tourId)
                .ToList();
        }

    }
}
