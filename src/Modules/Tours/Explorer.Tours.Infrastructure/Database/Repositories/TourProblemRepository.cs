using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourProblemRepository : ITourProblemRepository
    {
        private readonly ToursContext _dbContext;

        public TourProblemRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TourProblem GetOne(int tourProblemId)
        {
            return _dbContext.TourProblems
                .FirstOrDefault(t => t.Id == tourProblemId);
        }

        public TourProblem Update(TourProblem tourProblem)
        {
            try
            {
                _dbContext.Update(tourProblem);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return (TourProblem)tourProblem;
        }

        public List<TourProblemResponse> GetById(int tourtourProblemIdId)
        {
            //temporary
            return null;
        }

    }
}
