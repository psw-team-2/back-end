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
using Object = Explorer.Tours.Core.Domain.Object;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourProblemTouristCommandTests : BaseToursIntegrationTest
    {
        public TourProblemTouristCommandTests(ToursTestFactory factory) : base(factory) { }



        [Theory]
        [InlineData(-51, TourProblemCategory.CATEGORY1, TourProblemPriority.PRIORITY1, "Description 1", "2023-12-31T15:30:00Z", -41, false, false, -41, null, 200, 200)]
        [InlineData(-101, TourProblemCategory.CATEGORY1, TourProblemPriority.PRIORITY1, "", "2023-12-31T15:30:00Z", -41, false, false, -41, null, 400, 404)]
        public void Tests_CRUD(int id, TourProblemCategory category, TourProblemPriority priority, string description,
            string timeStamp, long tourId, bool isClosed, bool isResolved, long touristId,
            DateTime? deadlineTimeStamp, int expectedResponseCode, int expectedResponseCode2)
        {
            // Parse string timeStamp to UTC DateTime
            DateTime parsedTimeStamp = DateTime.Parse(timeStamp).ToUniversalTime();

            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var entity = new TourProblemDto
            {
                Id = id,
                ProblemCategory = category.ToString(),
                ProblemPriority = priority.ToString(),
                Description = description,
                TimeStamp = parsedTimeStamp, // Assign parsed timestamp here
                TourId = tourId,
                IsClosed = isClosed,
                IsResolved = isResolved,
                TouristId = touristId,
                DeadlineTimeStamp = deadlineTimeStamp
            };

            var result = (ObjectResult)controller.Create(entity).Result;

            result.ShouldNotBe(null);
            result.StatusCode.ShouldBe(expectedResponseCode);
            var resultEntity = result.Value as TourProblemDto;
            if (expectedResponseCode == 200)
            {
                resultEntity.Description.ShouldBe(description);
                var storedEntity = dbContext.TourProblems.FirstOrDefault(i => i.Description == resultEntity.Description);
                storedEntity.ShouldNotBeNull();
                dbContext.Entry(storedEntity).State = EntityState.Detached;
            }
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
