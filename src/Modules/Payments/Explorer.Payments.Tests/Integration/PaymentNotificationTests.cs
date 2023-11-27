using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator;
using Explorer.Payments.API.Public;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Shouldly;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class PaymentNotificationTests : BasePaymentsIntegrationTest
    {
        public PaymentNotificationTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new PaymentNotificationDto
            {
                UserId = 1,
                AdventureCoin = 200,
                Status = 0
            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;
            var createdNotification = result.Value as PaymentNotificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.PaymentNotifications.FirstOrDefault(p => p.Id == createdNotification.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(Core.Domain.PaymentNotification.NotificationStatus.Unread);
            storedEntity.UserId.ShouldBe(newEntity.UserId);
            storedEntity.AdventureCoin.ShouldBe(newEntity.AdventureCoin);
        }
        private static PaymentNotificationController CreateController(IServiceScope scope)
        {
            return new PaymentNotificationController(scope.ServiceProvider.GetRequiredService<IPaymentNotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
