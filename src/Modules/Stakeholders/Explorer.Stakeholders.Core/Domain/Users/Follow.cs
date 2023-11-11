using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Follow : ValueObject
    {
        public long ProfileId { get; init; }
        public long FollowerId { get; init; }

        [JsonConstructor]
        public Follow(long profileId, long followerId)
        {
            ProfileId = profileId;
            FollowerId = followerId;
            Validate();
        }

        private void Validate()
        {
            if (ProfileId == 0) throw new ArgumentException("Invalid ProfileId");
            if (FollowerId == 0) throw new ArgumentException("Invalid FollowerId");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProfileId;
            yield return FollowerId;
        }
    }
}
