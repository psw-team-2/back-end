using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class TourPreferenceTest : BaseStakeholdersIntegrationTest
    {
        public TourPreferenceTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Create_preference()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController(scope);
            var tourPreference = new TourPreferenceDto
            {
                Id = 1,
                TouristId = 1,
                Difficulty = 2,
                BoatRating = 2,
                WalkingRating = 2,
                CarRating = 2,
                BicycleRating = 2,
                Tags = new List<string> { "one", "two", "three" }
            };

            // Act
            var tourPreferenceResponse = ((ObjectResult)controller.Create(tourPreference).Result).Value as TourPreferenceDto;

            // Assert - Response
            tourPreferenceResponse.ShouldNotBeNull();
            tourPreferenceResponse.Id.ShouldNotBe(0);


            // Assert - Database
            dbContext.ChangeTracker.Clear();
            var storedTourPreference = dbContext.TourPreferences.FirstOrDefault(tp => tp.Id == tourPreference.Id);
            storedTourPreference.ShouldNotBeNull();
            storedTourPreference.CarRating.ShouldBe(2);
            storedTourPreference.BoatRating.ShouldBe(2);
            storedTourPreference.Tags.ShouldBe(new List<string> { "one", "two", "three" });
        }

        private static TourPreferenceController CreateController(IServiceScope scope)
        {
            return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>());
        }
    }
}
