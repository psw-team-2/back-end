using Explorer.API.Controllers.Administrator;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
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
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newCart = new ShoppingCartDto
            {
                Id = -2,
                UserId = -22,
                Items = new List<int> { 1 },
                TotalPrice = 0

            };
            var bundle = new BundleDto
            {
                Id = -5,
                UserId = -11,
                Name = "bundle5",
                Price = 500,
                Tours = new List<int> { 2 },
                Status = BundleStatus.Published
            };


            // Act
            //korsti pravu bazu za bundle 
            var result = ((ObjectResult)controller.AddBundleItem(newCart, 1).Result);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(t => t.Id == newCart.Id);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void AddNewBundleItem_Fails()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newCart = new ShoppingCartDto
            {
                Id = -1,
                UserId = -21,
                Items = new List<int>(),
                TotalPrice = 0

            };
            var bundle = new BundleDto
            {
                Id = -2,
            };


            // Act
            var result = ((ObjectResult)controller.AddBundleItem(newCart, bundle.Id).Result);

            // Assert - Response
            result.StatusCode.ShouldBe(404);
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
