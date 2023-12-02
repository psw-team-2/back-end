using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
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
    public class PurchaseReportTests : BasePaymentsIntegrationTest
    {
        public PurchaseReportTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {           
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newOrder = new OrderItemDto
            {
                ItemId = 2,
                ItemName = "Test",
                Price = 50,
                ShoppingCartId = 1,
                IsBought = false,
                IsBundle = false
            };

            List<OrderItemDto> orders = new List<OrderItemDto>();
            orders.Add(newOrder);

            // Act
            var result = (OkResult)controller.Create(orders,1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            // Assert - Database
            var storedEntity = dbContext.PurchaseReports.FirstOrDefault(t => t.TourId == newOrder.ItemId && t.UserId == 1);
            storedEntity.ShouldNotBeNull();
            storedEntity.AdventureCoin.ShouldBe(newOrder.Price);
        }

        [Fact]
        public void GetByPurchaseReportsTourist()
        {
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = controller.GetPurchaseReportsByTouristId(1);

            // Assert - Response
            result.ShouldNotBeNull();

            // Assert - Database
        }


        private static PurchaseReportController CreateController(IServiceScope scope)
        {
            return new PurchaseReportController(scope.ServiceProvider.GetRequiredService<IPurchaseReportService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
