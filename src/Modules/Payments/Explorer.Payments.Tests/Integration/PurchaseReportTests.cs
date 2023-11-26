using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
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
            var newEntity = new PurchaseReportDto
            {
                UserId = 1,
                TourId = 1,
                AdventureCoin = 200
            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            // Assert - Database
            var storedEntity = dbContext.PurchaseReports.FirstOrDefault(t => t.UserId == newEntity.UserId && t.TourId == newEntity.TourId);
            storedEntity.ShouldNotBeNull();
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
