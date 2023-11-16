using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class CheckpointVisited : Entity
    {
        public int UserId { get; private set; }
        public long CheckpointId { get; private set; }
        public DateTime Time { get; private set; }

        public CheckpointVisited(int userId, long checkpointId, DateTime time)
        {
            UserId = userId;
            CheckpointId = checkpointId;
            Time = time;
        }
    }
}
