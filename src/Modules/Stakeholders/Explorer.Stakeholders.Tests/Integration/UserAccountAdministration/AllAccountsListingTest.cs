using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.UserAccountAdministration
{
    [Collection("Sequential")]
    public class AllAccountsListingTest : BaseStakeholdersIntegrationTest
    {
        public AllAccountsListingTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<UserAccountDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(7);
            result.TotalCount.ShouldBe(7);
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
