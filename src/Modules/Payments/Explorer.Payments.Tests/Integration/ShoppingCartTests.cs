using Explorer.API.Controllers.Administrator;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
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
    public class ShoppingCartTests : BasePaymentsIntegrationTest
    {
        public ShoppingCartTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void AddNewBundleItem()
        {
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newCart = new ShoppingCartDto
            {
                Id = -1,
                UserId = -1,
                Items = new List<int>(),
                TotalPrice = 0

            };
            var newOrder = new OrderItemDto
            {
                ItemId = -1,
                ItemName = "Test",
                Price = 50,
                ShoppingCartId = 1,
                IsBought = false,
                IsBundle = true
            };


            // Act
            var result = ((ObjectResult)controller.AddBundleItem(newCart,newOrder.ItemId).Result);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            // Assert - Database
            var storedEntity = dbContext.OrderItems.FirstOrDefault(i => i.ItemId == newOrder.ItemId);
            storedEntity.ShouldNotBeNull();
            storedEntity.IsBundle.ShouldBe(newOrder.IsBundle);
        }



        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
