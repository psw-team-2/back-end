using Explorer.BuildingBlocks.Core.UseCases;
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
    public class TourReviewRepository : ITourReviewRepository
    {
        private readonly ToursContext _context;

        public TourReviewRepository(ToursContext context)
        {
            _context = context;
        }

        public TourPurchaseToken GetPurchaseToken(int tourId, int userId)
        {
            return _context.TourPurchaseToken
                .Where(pt => pt.TourId == tourId && pt.UserId == userId)
                .FirstOrDefault();
        }
        public TourReview Create(TourReview entity)
        {
            _context.TourReview.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(long id)
        {
            var entity = _context.TourReview.Find(id);
            if (entity != null)
            {
                _context.TourReview.Remove(entity);
                _context.SaveChanges();
            }
        }

        public TourReview Get(long id)
        {
            return _context.TourReview.Find(id);
        }


        public IEnumerable<TourReview> GetReviewsForTour(int tourId)
        {
            return _context.TourReview.Where(review => review.TourId == tourId).ToList();
        }

        public TourReview Update(TourReview entity)
        {
            _context.TourReview.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public List<TourReview> GetByTourId(int tourId)
        {
            return _context.TourReview
                .Where(tr => tr.TourId == tourId)
                .ToList();
        }
    }

}
