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
        public string Username { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public BlogStatus Status { get; init; }
        public string Image { get; init; }
        public List<BlogComment>? BlogComments { get; init; }    

        public UserBlog(long userId, string username, string title, string description, DateTime creationTime, BlogStatus status, string image)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentException ("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            UserId = userId;
            Username = username;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Status = status;
            Image = image;
            
        }
        public void AddComment(BlogComment blogComment)
        {
            BlogComments.Add(blogComment);
        }
    }

    
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }




}
