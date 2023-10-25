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
        public BlogStatus Status { get; init; }
        public string Image { get; init; }

        public UserBlog(long userId, string title, string description, DateTime creationTime, BlogStatus status, string image)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentException ("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            UserId = userId;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Status = status;
            Image = image;
        }
    }

    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }
}
