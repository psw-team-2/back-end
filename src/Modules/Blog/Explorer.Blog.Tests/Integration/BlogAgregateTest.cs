using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.API.Controllers;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Dtos;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogAgregateTest : BaseBlogIntegrationTest
    {
        public BlogAgregateTest(BlogTestFactory factory) : base(factory) { }

        // Add your test methods for blog functionalities similar to TourAggregateTest

//        [Theory]
//        [MemberData(nameof(BlogData))]
//        public void Adds_comment_to_blog(int commentId, UserBlogDto blog, int expectedStatusCode)
//        {
//            // Arrange
//            using var scope = Factory.Services.CreateScope();
//            var tourController = CreateBlogController(scope);
//            var checkpointController = CreateBlogController(scope);
//            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

//            dbContext.Database.BeginTransaction();

            // Act
            

//        }

        public static IEnumerable<object[]> BlogData()
        {
            yield return new object[]
                {
                    -1,
                    new UserBlogDto
                    {
                        Id = -1,
                        UserId = -2,
                        Username = "User123",
                        Title = "Blog Title One",
                        Description = "Description One",
                        CreationTime = DateTime.UtcNow,
                        Status = BlogStatus.Published,
                        Image = "Image URL",
                    },
                    new List<RatingDto>
                    {
                        new RatingDto { isUpvote = true, UserId = -3 },
                        new RatingDto { isUpvote = false, UserId = -4 }
                    },
                    new List<BlogCommentDto>
                    {
                        new BlogCommentDto
                        { 
                            Id = -1, 
                            UserId = -3, 
                            Username = "Commenter1",
                            Text = "Comment One", 
                            CreationTime = DateTime.UtcNow, 
                            LastModification = DateTime.UtcNow
                        }, 
                        new BlogCommentDto 
                        { 
                            Id = -2, 
                            UserId = -4,
                            Username = "Commenter2", 
                            Text = "Comment Two", 
                            CreationTime = DateTime.UtcNow, 
                            LastModification = DateTime.UtcNow
                        }
                    },
                    200
            };


            yield return new object[]
            {
                -2,
                new UserBlogDto
                {
                    Id = -2,
                    UserId = -2,
                    Username = "User123",
                    Title = "Blog Title One",
                    Description = "Description One",
                    CreationTime = DateTime.UtcNow,
                    Status = BlogStatus.Draft,
                    Image = "Image URL",
                },
                new List<RatingDto>
                {
                    new RatingDto { isUpvote = true, UserId = -3 },
                    new RatingDto { isUpvote = false, UserId = -4 }
                },
                new List<BlogCommentDto>
                {
                    new BlogCommentDto
                    {
                        Id = -2,
                        UserId = -3,
                        Username = "Commenter1",
                        Text = "Comment Three",
                        CreationTime = DateTime.UtcNow,
                        LastModification = DateTime.UtcNow
                    },
                    new BlogCommentDto
                    {
                        Id = -3,
                        UserId = -4,
                        Username = "Commenter2",
                        Text = "Comment Four",
                        CreationTime = DateTime.UtcNow,
                        LastModification = DateTime.UtcNow
                    }
                },
                400
            };




        }

        
        private static UserBlogController CreateBlogController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
            return new UserBlogController(scope.ServiceProvider.GetRequiredService<IUserBlogService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>(), context)
            {
                ControllerContext = BuildContext("-1")
            };
        }



    }
}
