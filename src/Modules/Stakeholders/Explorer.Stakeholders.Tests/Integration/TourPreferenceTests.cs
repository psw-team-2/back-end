using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class TourPreferenceTests : BaseStakeholdersIntegrationTest
    {
        public TourPreferenceTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new TourPreferenceDto
            {
                Id = 1,
                TouristId = 1,
                WalkingRating = 1,
                BoatRating =1,
                BicycleRating  =1, 
                CarRating = 1,
                Difficulty = 3,
                Tags = new List<string>{"tag1", "tag2"}
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourPreferenceDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Difficulty.ShouldBe(newEntity.Difficulty);

            // Assert - Database
            var storedEntity = dbContext.TourPreferences.FirstOrDefault(i => i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.WalkingRating.ShouldBe(result.WalkingRating);
        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourPreferenceDto
            {
                Difficulty = 2
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        //[Fact]
        //public void Updates()
        //{
        //    // Arrange
        //    using var scope = Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        //    var updatedEntity = new TourPreferenceDto
        //    {
        //        Id = 1,
        //        TouristId = 1,
        //        WalkingRating = 2,
        //        BoatRating = 2,
        //        BicycleRating = 2,
        //        CarRating = 2,
        //        Difficulty = 3,
        //        Tags = new List<string> { "tag1", "tag2" }
        //    };

        //    // Act
        //    var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourPreferenceDto;

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldBe(1);
        //    result.TouristId.ShouldBe(updatedEntity.TouristId);
        //    result.BoatRating.ShouldBe(updatedEntity.BoatRating);
        //    // Assert - Database
        //    var storedEntity = dbContext.TourPreferences.FirstOrDefault(i => i.Id == 1);
        //    storedEntity.ShouldNotBeNull();
        //}

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourPreferenceDto
            {
                Id = -1332,
                TouristId = 2,
                WalkingRating = 2,
                BoatRating = 2,
                BicycleRating = 2,
                CarRating = 2,
                Difficulty = 3,
                Tags = new List<string> { "tag1", "tag2" }
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        //[Fact]
        //public void Deletes()
        //{
        //    // Arrange
        //    using var scope = Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        //    // Act
        //    var result = (OkResult)controller.Delete(1);

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.StatusCode.ShouldBe(200);

        //    // Assert - Database
        //    var storedCourse = dbContext.TourPreferences.FirstOrDefault(i => i.Id == 1);
        //    storedCourse.ShouldBeNull();
        //}

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
        private static TourPreferenceController CreateController(IServiceScope scope)
        {
            return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
