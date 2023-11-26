using Explorer.API.Controllers.Administrator;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class WalletCommandTests : BasePaymentsIntegrationTest
    {
        public WalletCommandTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void AddAC()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var updatedEntity = new WalletDto
            { 
                Id = -1,
                UserId = -21,
                Username = "turista1",
                AC = 100,
            };
            var result = ((ObjectResult)controller.AddAC(updatedEntity).Result);
            var wallet = result.Value as WalletDto;
            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            wallet.Username.ShouldBe(updatedEntity.Username);

            // Assert - Database
            var storedEntity = dbContext.Wallets.FirstOrDefault(i => i.AC == 150);
            storedEntity.ShouldNotBeNull();
            
            var oldEntity = dbContext.Wallets.FirstOrDefault(i => i.AC == 50);
            oldEntity.ShouldBeNull();
        }


        private static WalletController CreateController(IServiceScope scope)
        {
            
            return new WalletController(scope.ServiceProvider.GetRequiredService<IWalletService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
