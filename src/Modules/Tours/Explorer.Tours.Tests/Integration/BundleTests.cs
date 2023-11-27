using Explorer.API.Controllers.Author;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class BundleTests : BaseToursIntegrationTest
    {
        public BundleTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new BundleDto
            {
                Id = 1,
                UserId = -11,
                Name = "Novi",
                Price = 0,
                Status = BundleDto.BundleStatus.Draft,
                Tours = new List<TourDto>()

            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Id == newEntity.Id);
            storedEntity.ShouldNotBeNull();

        }

        [Fact]
        public void AddTour()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            List<TourDto> tours = new List<TourDto>();
            var tourDto = new TourDto
            {
                Id = -1,
                Name = "New Tour",
                Description = "Description of the new tour",
                Status = AccountStatus.DRAFT,
                Difficulty = 3,
                Price = 50.0,
                Tags = new List<string> { "Adventure", "Hiking" },
                Equipment = new List<int> { 1, 2 },
                CheckPoints = new List<long> { 123, 456 },
                AuthorId = -11,
            };
            tours.Add(tourDto);
            var bundle = new BundleDto
            {
                Id = 1,
                UserId = -11,
                Name = "Novi",
                Price = 0,
                Status = BundleDto.BundleStatus.Draft,
                Tours = new List<TourDto>()
            };

            // Act
            var result = (ObjectResult)controller.AddItem(bundle,tourDto.Id).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Id == bundle.Id);
            storedEntity.ShouldNotBeNull();
        }


        private static BundleController CreateController(IServiceScope scope)
    {
        return new BundleController(scope.ServiceProvider.GetRequiredService<IBundleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
    
}
}
