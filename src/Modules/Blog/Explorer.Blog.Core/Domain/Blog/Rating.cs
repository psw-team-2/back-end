using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.Blog
{
    public class Rating : ValueObject
    {
        public bool isUpvote { get; init; } //veliko
        public long UserId { get; init; }
        public DateTime CreationTime { get; init; }

        [JsonConstructor]
        public Rating(bool isUpvote, long userId, DateTime creationTime)
        {
            this.isUpvote = isUpvote;
            UserId = userId;
            CreationTime = creationTime;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return isUpvote;
            yield return UserId;
            yield return CreationTime;
        }

        

    }
}
