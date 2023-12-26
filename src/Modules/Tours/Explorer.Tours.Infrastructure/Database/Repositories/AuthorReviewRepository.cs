using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class AuthorReviewRepository: IAuthorReviewRepository
    {
        private readonly ToursContext _context;

        public AuthorReviewRepository(ToursContext context)
        {
            _context = context;
        }

        public AuthorReview Create(AuthorReview entity)
        {
            _context.AuthorReview.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(long id)
        {
            var entity = _context.AuthorReview.Find(id);
            if (entity != null)
            {
                _context.AuthorReview.Remove(entity);
                _context.SaveChanges();
            }
        }

        public AuthorReview Get(long id)
        {
            return _context.AuthorReview.Find(id);
        }

        public AuthorReview Update(AuthorReview entity)
        {
            _context.AuthorReview.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<AuthorReview> GetAuthorReviews(int authorId)
        {
            return _context.AuthorReview
                .Where(ar => ar.AuthorId == authorId)
                .ToList();
        }
    }
}