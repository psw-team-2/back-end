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
                Image = "slika4"
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
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new UserBlogDto
            {
                Image="test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
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

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Izmenjen naslov");
            storedEntity.ShouldNotBeNull();
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            storedEntity.Title.ShouldBe(updatedEntity.Title);
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            storedEntity.Image.ShouldBe(updatedEntity.Image);
            var oldEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Novi Sad");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new UserBlogDto
            {
                Id = -1000,
                UserId = 15,
                Title = "Izmenjen naslov",
                Description = "Izmenjen opis",
                Image = "izmenjena slika",
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Blogs.FirstOrDefault(i => i.Id == 3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
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
