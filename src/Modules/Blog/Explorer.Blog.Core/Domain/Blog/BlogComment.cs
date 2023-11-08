using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.Blog
{
    public class BlogComment : Entity
    {
        public long UserId { get; init; }
        public string Username { get; init; }   
        public long BlogId { get; init; }
        public string Text { get; init; }
        public DateTime CreationTime { get; init; }
        public DateTime LastModification { get; init; }


        public BlogComment(long userId, string username, long blogId, string text, DateTime creationTime, DateTime lastModification)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid comment.");
            UserId = userId;
            Username = username;
            BlogId = blogId;
            Text = text;
            CreationTime = creationTime;
            LastModification = lastModification;
        }

    }
}
