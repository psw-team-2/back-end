using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourReviewCommandTests : BaseToursIntegrationTest
    {
        public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourReviewDto
            {
                Grade = 2,
                Comment = "Awesome",
                VisitDate = DateTime.SpecifyKind(DateTime.Parse("2023-10-10 13:00:00"), DateTimeKind.Utc),
                ReviewDate = DateTime.SpecifyKind(DateTime.Parse("2023-10-20 13:00:00"), DateTimeKind.Utc),
                Images = "IMG_20230418_093842"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Grade.ShouldBe(newEntity.Grade);
            result.Comment.ShouldBe(newEntity.Comment);
            result.VisitDate.ShouldBe(newEntity.VisitDate);
            result.ReviewDate.ShouldBe(newEntity.ReviewDate);
            result.Images.ShouldBe(newEntity.Images);

            // Assert - Database
            var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == newEntity.Comment);

            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Comment.ShouldBe(newEntity.Comment);
            storedEntity.VisitDate.ShouldBe(newEntity.VisitDate);
            storedEntity.ReviewDate.ShouldBe(newEntity.ReviewDate);
            storedEntity.Images.ShouldBe(newEntity.Images);
        }


        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourReviewDto
            {
                
                Comment = "Test"
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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourReviewDto
            {
                Id = -1,
                Grade = 2,
                Comment = "Very bad experience",
                UserId = 1,
                VisitDate = DateTime.SpecifyKind(DateTime.Parse("2023-10-10 13:00:00"), DateTimeKind.Utc),
                ReviewDate = DateTime.SpecifyKind(DateTime.Parse("2023-11-10 13:00:00"), DateTimeKind.Utc),
                Images = "slike"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Grade.ShouldBe(updatedEntity.Grade);
            result.Comment.ShouldBe(updatedEntity.Comment);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.VisitDate.ShouldBe(updatedEntity.VisitDate);
            result.ReviewDate.ShouldBe(updatedEntity.ReviewDate);
            result.Images.ShouldBe(updatedEntity.Images);

            // Assert - Database
            var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "Very bad experience");
            storedEntity.ShouldNotBeNull();
            storedEntity.Grade.ShouldBe(updatedEntity.Grade);
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            storedEntity.VisitDate.ShouldBe(updatedEntity.VisitDate);
            storedEntity.ReviewDate.ShouldBe(updatedEntity.ReviewDate);
            var oldEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "Okay");
            oldEntity.ShouldBeNull();
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourReviewDto
            {
                Id = -1000,
                Grade = 4,
                Comment = "Test",
                Images = "Test"
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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TourReview.FirstOrDefault(i => i.Id == -3);
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

        private static TourReviewController CreateController(IServiceScope scope)
        {
            var environment = new Mock<IWebHostEnvironment>().Object; // You might need to create a mock environment for testing.

            return new TourReviewController(
                scope.ServiceProvider.GetRequiredService<ITourReviewService>(),
                environment)
            {
                ControllerContext = BuildContext("-1")
            };
        }


    }


}
