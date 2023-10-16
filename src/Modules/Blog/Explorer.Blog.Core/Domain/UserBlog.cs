using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class UserBlog : Entity
    {
        public long UserId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public string Image { get; init; }
        public BlogStatus Status { get; init; }

        public UserBlog(long userId, string title, string description, DateTime creationTime, string image, BlogStatus status)
        {
            UserId = userId;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Image = image;
            Status = status;
        }
    }

    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }
}
