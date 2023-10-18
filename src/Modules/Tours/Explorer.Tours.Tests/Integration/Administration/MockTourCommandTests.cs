using Explorer.API.Controllers.Administrator.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;


namespace Explorer.Tours.Tests.Administration
{
    [Collection("Sequential")]
    public class MockTourCommandTests : BaseToursIntegrationTest
    {
        public MockTourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new MockTourDto()
            {
                TourInfo = "Mock Tour Info"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as MockTourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourInfo.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.MockTours.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new MockTourDto()
            {
                TourInfo = "Mock Tour Info Create"
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
            var updatedEntity = new MockTourDto()
            {
                Id = -1,
                TourInfo = "Mock Tour Info Update"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as MockTourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.TourInfo.ShouldNotBeNull();
                // Assert - Database
            var storedEntity = dbContext.MockTours.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourInfo.ShouldBe(updatedEntity.TourInfo);
            //This should be revised, could have to implement oldEntity
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new MockTourDto()
            {
                Id = -1000,
                TourInfo = "Mock Tour Info Update"
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
            var storedCourse = dbContext.Equipment.FirstOrDefault(i => i.Id == -3);
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


        private static MockTourController CreateController(IServiceScope scope)
        {
            return new MockTourController(scope.ServiceProvider.GetRequiredService<IMockTourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
