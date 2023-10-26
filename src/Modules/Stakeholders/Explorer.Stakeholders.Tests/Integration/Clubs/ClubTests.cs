using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Infrastructure.Database;

namespace Explorer.Stakeholders.Tests.Integration.Clubs
{
    public class ClubTests : BaseStakeholdersIntegrationTest
    {
        public ClubTests(StakeholdersTestFactory factory) : base(factory) { }

        /*[Fact]*/
        public void Successfully_creates_club()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var creationSubmission = new ClubDto { Id = 1, Name = "Club1", Description = "Desc1", OwnerId = 1, MemberIds = new List<long>() };
            var loginSubmission = new CredentialsDto { Username = "turista1@gmail.com", Password = "turista1" };

            // Act
            var response = ((ObjectResult)controller.Create(creationSubmission).Result).Value as ClubDto;

            // Assert
            response.ShouldNotBeNull();
            /*response.Id.ShouldBe(-21);
            var decodedAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);
            var personId = decodedAccessToken.Claims.FirstOrDefault(c => c.Type == "personId");
            personId.ShouldNotBeNull();
            personId.Value.ShouldBe("-21");*/
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>(), environment)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
