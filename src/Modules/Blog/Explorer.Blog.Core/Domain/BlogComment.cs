using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogComment : Entity
    {
        public long UserId { get; init; }
        public long BlogId { get; init; }
        public string Text { get; init; }
        public DateTime CreationTime { get; init; }
        public DateTime LastModification { get; init; }


        public BlogComment(long userId, long blogId, string text, DateTime creationTime, DateTime lastModification)
        {
            UserId = userId;
            BlogId = blogId;
            Text = text;
            CreationTime = creationTime;
            LastModification = lastModification;
        }

    }
}
