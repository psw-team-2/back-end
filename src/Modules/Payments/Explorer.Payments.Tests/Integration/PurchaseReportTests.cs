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
                TourId = 2,
                TourName = "Test",
                Price = 50,
                ShoppingCartId = 1,
                IsBought = false
            };

            List<OrderItemDto> orders = new List<OrderItemDto>();
            orders.Add(newOrder);

            // Act
            var result = (OkResult)controller.Create(orders,1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            // Assert - Database
            var storedEntity = dbContext.PurchaseReports.FirstOrDefault(t => t.TourId == newOrder.TourId && t.UserId == 1);
            storedEntity.ShouldNotBeNull();
            storedEntity.AdventureCoin.ShouldBe(newOrder.Price);
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
