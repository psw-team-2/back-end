using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublicRequest : Entity
    {
        public long EntityId { get; private set; }
        public long AuthorId { get; private set; }
        public string Comment { get; private set; }
        public bool IsCheckPoint { get; private set; }
        public bool IsNotified { get; private set; }
    }
}
