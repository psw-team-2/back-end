using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.Blog
{
    public class UserBlog : Entity
    {
        public long UserId { get; init; }
        public string Username { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public BlogStatus Status { get; set; }
        public string Image { get; init; }
        public BlogCategory Category { get; init; }
        public List<Rating> Ratings { get; init; }

        public List<BlogComment> BlogComments { get; init; }

        public UserBlog(long userId, string username, string title, string description, DateTime creationTime, BlogStatus status, string image, BlogCategory category)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            UserId = userId;
            Username = username;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Status = status;
            Image = image;
            Category = category;
            Ratings = new List<Rating>();
            BlogComments = new List<BlogComment>();
        }

        public void AddRating(Rating rating)
        {
            var existingRating = Ratings?.FirstOrDefault(r => r.UserId == rating.UserId);

            if (existingRating != null)
            {
                int index = Ratings.IndexOf(existingRating);
                if (index >= 0)
                {
                    Ratings.RemoveAt(index);
                    Ratings.Insert(index, rating);
                }
            }
            else
            {
                Ratings.Add(new Rating(rating.isUpvote, rating.UserId, rating.CreationTime));
            }

            CheckBlogStatus();
            
        }

        public void CheckBlogStatus()
        {
            int ratingsCount = GetRatingsCount();
            int commentsCount = BlogComments.Count();

            if (ratingsCount <= -10)
            {
                Status = BlogStatus.Closed;
            }
            else if (ratingsCount > 100 || commentsCount > 10)
            {
                Status = BlogStatus.Active;
            }
            else if (ratingsCount > 500 && commentsCount > 30) 
            {
                Status = BlogStatus.Famous;
            }

           
        }
        public int GetRatingsCount()
        {
            int upvotes = 0;
            int downvotes = 0;
            foreach(var rating in Ratings)
            {
                if( rating.isUpvote == true)
                {
                    upvotes++;
                }
                else
                {
                    downvotes++;
                }
            }

            return upvotes - downvotes;
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
        Closed,
        Active,
        Famous
    }

    public enum BlogCategory
    {
        Destinations,
        Travelogues,
        Activities,
        Gastronomy,
        Tips,
        Culture,
        Accommodation
    }


}
