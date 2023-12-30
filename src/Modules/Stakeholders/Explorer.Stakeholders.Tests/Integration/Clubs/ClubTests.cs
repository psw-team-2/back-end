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
using Explorer.Blog.API.Dtos;
using Explorer.Stakeholders.Infrastructure.Database;

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


        /*
        [Theory]
        public void Creates_club(int id, string name, string description, string imageUrl, long ownerId,
            List<long> memberIds)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var club = new ClubDto(){Id = id, Name = name, Description = description, ImageUrl = imageUrl, OwnerId = ownerId, MemberIds = new List<long>(memberIds) };



            var result = (ObjectResult)controller.Create(club);
        }
        */



        public static IEnumerable<object[]> ClubData()
        {
            yield return new object[]
            {
                new ClubDto()
                {
                    Id = -41,
                    Name = "Test Club 1",
                    Description = "Club Description",
                    ImageUrl = "Image Url",
                    OwnerId = -41,
                    MemberIds = {-41, -42, -43}
                },
                200            };
            yield return new object[]
            {
                new ClubDto()
                {
                    Id = -42,
                    Name = "",
                    Description = "",
                    ImageUrl = "",
                    OwnerId = -42,
                    MemberIds = {-41, -42, -43}
                },
                400
            };

            yield return new object[]
            {
                new ClubDto()
                {
                    Id = -43,
                    Name = "Test Club 3",
                    Description = "Club Description",
                    ImageUrl = "Image Url",
                    OwnerId = -43,
                    MemberIds = {-41, -42, -43}
                },
                200
            };
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
