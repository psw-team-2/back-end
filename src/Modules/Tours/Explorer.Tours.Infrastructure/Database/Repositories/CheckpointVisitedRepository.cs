using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class CheckpointVisitedRepository : ICheckpointVisitedRepository
    {
        private readonly ToursContext _dbContext;
        public CheckpointVisitedRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CheckpointVisited Add(CheckpointVisited entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public CheckpointVisited GetVisitedCheckpoint(int userId, int checkpointId)
        {
            return _dbContext.CheckpointVisited.FirstOrDefault(cp => cp.UserId == userId && cp.CheckpointId == checkpointId);
        }

        public List<CheckpointVisited> GetVisitedCheckpointsByUser(int userId)
        {
            return _dbContext.CheckpointVisited.Where(cp => cp.UserId == userId).ToList();
        }


        public List<CheckpointVisited> GetCheckpointsVisitedByIds(List<int> checkpointVisitedIds)
        {
            return _dbContext.CheckpointVisited.Where(cp => checkpointVisitedIds.Contains((int)cp.Id)).ToList();
        }

    }
}
