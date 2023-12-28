using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Public.Administration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Type = Explorer.Encounters.API.Dtos.Type;
using Microsoft.AspNetCore.Hosting;

namespace Explorer.Encounters.Tests.Integration
{

    [Collection("Sequential")]
    public class EncountersTests : BaseEncountersIntegrationTest
    {
        public EncountersTests(EncountersTestFactory factory) : base(factory){}


        [Theory]
        [InlineData(-51, "Encounter 1", "Description 1", 45.14503, 19.50533, 100, Status.Active, Type.Social, 200)]
        [InlineData(-52, "", "", 0, 0, 0, null, null, 400)]
        public void Creates_encounter(int id, string name, string description, double latitude,
            double longiture, int xp, Status status, Type type, int expectedResponseCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();

            var encounter = new EncounterDto()
            {
                Id = id, Name = name,Description = description, Latitude = latitude,
                Longitude = longiture, XP = xp, Status = status, Type = type    
            };
            
            var result = (ObjectResult)controller.Create(encounter).Result;
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }



        private static EncounterController CreateController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
