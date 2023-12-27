using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Token : Entity
    {
        public long UserId { get; init; }
        public string Value { get; init; }
        public DateTime ExpirationTime { get; init; }

        public Token(long userId, string value, DateTime expirationTime)
        {
            UserId = userId;
            Value = value;
            ExpirationTime = expirationTime;
        }
    }
}
