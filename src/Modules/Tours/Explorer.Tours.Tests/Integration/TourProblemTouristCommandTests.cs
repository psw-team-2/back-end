using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourProblemTouristCommandTests : BaseToursIntegrationTest
    {
        public TourProblemTouristCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourProblemDto()
            {
                Id = -51,
                ProblemCategory = "CATEGORY 1",
                ProblemPriority = "PRIORITY 1",
                Description = "Test Problem Description",
                TimeStamp = DateTime.UtcNow,
                TourId = -41,
                IsClosed = false,
                IsResolved = false,
                TouristId = -41,
                DeadlineTimeStamp = null
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
                TourId = 0,
                IsClosed = false,
                TouristId = 0,
                DeadlineTimeStamp = null
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

/*        [Fact]
            public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourProblemDto()
            {
                Id = -51,
                ProblemCategory = "CATEGORY 2",
                ProblemPriority = "PRIORITY 2",
                Description = "Test Problem Description Updated",
                TimeStamp = DateTime.UtcNow,
                TourId = -41,
                IsClosed = false,
                IsResolved = false,
                TouristId = -41,
                DeadlineTimeStamp = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result).Value as TourProblemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.ProblemCategory.ShouldBe(updatedEntity.ProblemCategory);
            result.ProblemPriority.ShouldBe(updatedEntity.ProblemPriority);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.TourProblems.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            //This should be revised, could have to implement oldEntity
        }
*/


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
                TourId = -41,
                IsClosed = false,
                IsResolved = false,
                TouristId = -41,
                DeadlineTimeStamp = null
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }





        [Theory]
        [InlineData(-51, "Test Response", -41, -41, 200)]
        [InlineData(-52, "", -8000, -41, 400)]
        public void RespondToProblem_tests(int id, string response, int tourProblemId, int commenterId, int expectedResponseCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newTourProblemResponseEntity = new TourProblemResponseDto()
            {
                Id = id,
                Response = response,
                TimeStamp = DateTime.UtcNow,
                TourProblemId = tourProblemId,
                CommenterId = commenterId
            };

            var result =
                (ObjectResult)controller.RespondToProblem(tourProblemId, newTourProblemResponseEntity);

            var resultEntity = result.Value as TourProblemResponseDto;

            if (result.StatusCode == 200)
            {
                resultEntity.ShouldNotBeNull();
                resultEntity.TimeStamp.ShouldBe(newTourProblemResponseEntity.TimeStamp);
                resultEntity.Response.ShouldBe(newTourProblemResponseEntity.Response);
                resultEntity.CommenterId.ShouldBe(newTourProblemResponseEntity.CommenterId);
                resultEntity.TourProblemId.ShouldBe(newTourProblemResponseEntity.TourProblemId);
            }

            result.StatusCode.ShouldBe(expectedResponseCode);
        }


        [Theory]
        [InlineData(-1, 200)]
        public void GetProblemResponses_tests(int tourProblemId, int expectedResponseCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = (ObjectResult)controller.GetProblemResponses(tourProblemId).Result;

            var resultEntity = result.Value;

            resultEntity.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

        }
        

        private static TourProblemTouristController CreateController(IServiceScope scope)
        {
            return new TourProblemTouristController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourProblemResponseService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
