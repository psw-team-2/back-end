using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.UserAccountAdministration
{
    [Collection("Sequential")]
    public class AccountDeactivationTest : BaseStakeholdersIntegrationTest
    {
        public AccountDeactivationTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new UserAccountDto
            {
                Id = -21,
                Username = "turista1Updated@gmail.com",
                Email = "turista1@gmail.com",
                Password = "turista1",
                Role = UserRole.Tourist,
                IsActive = false,
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as UserAccountDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-21);
            result.Username.ShouldBe(updatedEntity.Username);
            result.Password.ShouldBe(updatedEntity.Password);
            result.Role.ShouldBe(updatedEntity.Role);
            result.IsActive.ShouldBe(updatedEntity.IsActive);

            // Assert - Database
            var storedEntity = dbContext.Users.FirstOrDefault(i => i.Username == "turista1Updated@gmail.com");
            storedEntity.ShouldNotBeNull();
            storedEntity.IsActive.ShouldBe(updatedEntity.IsActive);
            var oldEntity = dbContext.Users.FirstOrDefault(i => i.Username == "turista1@gmail.com");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new UserAccountDto
            {
                Id = -1000,
                Username = "turista1@gmail.com",
                Email = "turista1@gmail.com",
                Password = "turista1",
                Role = UserRole.Tourist,
                IsActive = false,
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (ObjectResult)controller.Delete(-23);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Users.FirstOrDefault(i => i.Id == -23);
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

        private static UserAccountController CreateController(IServiceScope scope)
        {
            return new UserAccountController(scope.ServiceProvider.GetRequiredService<IUserAccountAdministrationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
