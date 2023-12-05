using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Explorer.Encounters.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.Encounters.Tests.Integration
{
    [Collection("Sequential")]
    public class EncounterLevelProgressionTests: BaseEncountersIntegrationTest
    {
        public EncounterLevelProgressionTests(EncountersTestFactory factory) : base(factory) { }

        [Fact]
        public void ResolveEncounter_Success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();

            // Dodajte potrebne informacije o susretu i aktivnom susretu
            var encounterId = 1;
            var touristId = 1;
            var encounterXP = 10;

            var encounter = new EncounterDto() 
            {
                Id = -1,
                Name = "New Challenge", 
                Description = "Challenge Description", 
                Latitude = 0,
                Longitude = 0, 
                XP = encounterXP, 
                Status = API.Dtos.Status.Active, 
                Type = API.Dtos.Type.Misc, 
                Mandatory = false, 
                PeopleCount = 1, 
                Range = 0.0f, 
                Image = null 
            };

            var activeEncounter = new ActiveEncounterDto()
            {
                Id = -1,
                EncounterId = encounterId,
                TouristId = touristId,
                State = API.Dtos.State.Done,
                End = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Update(encounter).Result).Value as EncounterDto;
            //var resultEncounter = ((ObjectResult)controller.Update(activeEncounter).Result).Value as ActiveEncounterDto;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Challenges.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
        }

      /*  [Fact]
        public void CheckForLevelUp_LevelUp_Success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();

            var encounterId = 1;
            var touristId = 1;

            // Act
            var result = (ObjectResult)controller.CheckForLevelUp(encounterId, touristId).Result;

            // Assert - Response
            result.ShouldNotBeNull();

        }*/

        [Fact]
        public void CreateNewChallenge_Success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();

            var newEncounter = new EncounterDto()
            {
                Id = -1,
                Name = "New Challenge",
                Description = "Challenge Description",
                Latitude = 0,
                Longitude = 0,
                XP = 0,
                Status = API.Dtos.Status.Active,
                Type = API.Dtos.Type.Misc,
                Mandatory = false,
                PeopleCount = 1,
                Range = 0.0f,
                Image = null
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEncounter).Result)?.Value as EncounterDto;

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Challenges.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        private static EncounterController CreateController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>() /*scope.ServiceProvider.GetRequiredService<IActiveEncounterService>()*/)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }

}