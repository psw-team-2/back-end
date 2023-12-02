using Explorer.API.Controllers.Administrator;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
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
    public class TourPurchaseTokenTest : BasePaymentsIntegrationTest
    {

        public TourPurchaseTokenTest(PaymentsTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates_TourPurchaseToken_ForBundleItem()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var OrderItems = new List<OrderItemDto>();
            var orderItem1 = new OrderItemDto
            {
               Id = -1,
               ItemId = -1,
               Price =  100,
               ShoppingCartId = -1,
               IsBought = false,
               IsBundle = true
            };
            var orderItem2 = new OrderItemDto
            {
                Id = -2,
                ItemId = -2,
                Price = 200,
                ShoppingCartId = -1,
                IsBought = false,
                IsBundle = true
            };
            var orderItem3 = new OrderItemDto
            {
                Id = -3,
                ItemId = -3,
                Price = 300,
                ShoppingCartId = -1,
                IsBought = false,
                IsBundle = true
            };
            OrderItems.Add(orderItem1);
            OrderItems.Add(orderItem2);
            OrderItems.Add(orderItem3);
            int userId = -21;

            var result = ((ObjectResult)controller.CreateTourPurchaseToken(OrderItems, userId))?.Value as PagedResult<TourPurchaseTokenDto>;


            // Assert
            
        }

        private static TourPurchaseTokenController CreateController(IServiceScope scope)
        {

            return new TourPurchaseTokenController(scope.ServiceProvider.GetRequiredService<ITourPurchaseTokenService>(), scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
