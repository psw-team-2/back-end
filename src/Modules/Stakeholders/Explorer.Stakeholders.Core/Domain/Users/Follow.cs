using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Follow : Entity
    {
        public long ProfileId { get; init; }
        public long FollowerId { get; init; }

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
    }
}
