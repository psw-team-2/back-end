using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new UserBlogDto
            {
                UserId = 1,
                Title = "Planinarenje",
                Description = "Tara",
                CreationTime = DateTime.UtcNow,
                Status = BlogStatus.Published,
                Image = "slika4",
                Username = "ana"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as UserBlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new UserBlogDto
            {
                Id = 1,
                UserId = 15,
                Title = "Izmenjen naslov",
                Description = "Izmenjen opis",
                Image = "izmenjena slika",
                Username = "ana",
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as UserBlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.Title.ShouldBe(updatedEntity.Title);
            result.Description.ShouldBe(updatedEntity.Description);
            result.Status.ShouldBe(updatedEntity.Status);
            result.Image.ShouldBe(updatedEntity.Image);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.Username.ShouldBe(updatedEntity.Username);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Izmenjen naslov");
            storedEntity.ShouldNotBeNull();
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            storedEntity.Username.ShouldBe(updatedEntity.Username);
            storedEntity.Title.ShouldBe(updatedEntity.Title);
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            storedEntity.Image.ShouldBe(updatedEntity.Image);
            var oldEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Novi Sad");
            oldEntity.ShouldBeNull();
        }

        

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedCourse = dbContext.Blogs.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

       

        private static UserBlogController CreateController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
            return new UserBlogController(scope.ServiceProvider.GetRequiredService<IUserBlogService>(), environment, context)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
