using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourQueryTests : BaseToursIntegrationTest
    {
        public TourQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void RetrievesAllTours()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBeGreaterThan(1);
            result.TotalCount.ShouldBeGreaterThan(1);
        }

        [Fact]
        public void RetrievesOneTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            int tourIdToRetrieve = -1; // Replace with a valid tour ID from your test data

            // Act
            var result = ((ObjectResult)controller.Get(tourIdToRetrieve).Result)?.Value as TourDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(tourIdToRetrieve);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IPublicRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
