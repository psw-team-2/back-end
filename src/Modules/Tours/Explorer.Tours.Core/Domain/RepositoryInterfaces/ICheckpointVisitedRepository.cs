using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointVisitedRepository
    {
        public CheckpointVisited Add(CheckpointVisited entity);
        public CheckpointVisited GetVisitedCheckpoint(int userId, int checkpointId);
        public List<CheckpointVisited> GetVisitedCheckpointsByUser(int userId);
        public List<CheckpointVisited> GetCheckpointsVisitedByIds(List<int> checkpointVisitedIds);
    }
}
