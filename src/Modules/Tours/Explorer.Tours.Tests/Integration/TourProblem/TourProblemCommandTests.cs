using Explorer.API.Controllers.Administrator.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;


namespace Explorer.Stakeholders.Tests.Integration.TourProblem
{
    [Collection("Sequential")]
    public class TourProblemCommandTests : BaseToursIntegrationTest
    {
        public TourProblemCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourProblemDto()
            {
                Id = -15,
                ProblemCategory = "CATEGORY 1",
                ProblemPriority = "PRIORITY 1",
                Description = "Test Problem Description",
                TimeStamp = DateTime.UtcNow,
                MockTourId = 19
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourProblemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.ProblemCategory.ShouldBe(newEntity.ProblemCategory);
            result.ProblemPriority.ShouldBe(newEntity.ProblemPriority);
            result.Description.ShouldBe(newEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.TourProblems.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourProblemDto()
            {
                Description = "",
                ProblemCategory = "",
                ProblemPriority = "",
                TimeStamp = DateTime.UtcNow,
                MockTourId = 0
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
            var updatedEntity = new TourProblemDto()
            {
                Id = -15,
                ProblemCategory = "CATEGORY 2",
                ProblemPriority = "PRIORITY 2",
                Description = "Test Problem Description Updated",
                TimeStamp = DateTime.UtcNow,
                MockTourId = -1
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourProblemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-15);
            result.MockTourId.ShouldBe(-1);
            result.ProblemCategory.ShouldBe(updatedEntity.ProblemCategory);
            result.ProblemPriority.ShouldBe(updatedEntity.ProblemPriority);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.TourProblems.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            //This should be revised, could have to implement oldEntity
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourProblemDto()
            {
                Id = -1000,
                Description = "Test Update Fail Description",
                ProblemCategory = "CATEGORY -1",
                ProblemPriority = "PRIORITY -1",
                TimeStamp = DateTime.UtcNow,
                MockTourId = 2000
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
            var result = (OkResult)controller.Delete(-15);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Equipment.FirstOrDefault(i => i.Id == -15);
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


        private static TourProblemController CreateController(IServiceScope scope)
        {
            return new TourProblemController(scope.ServiceProvider.GetRequiredService<ITourProblemService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
