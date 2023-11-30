using Explorer.API.Controllers.Author;
using Explorer.Payments.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
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
                Id = -2,
                UserId = -11,
                Name = "Novi",
                Price = 0,
                Status = BundleStatus.Draft,
                Tours = new List<int>()

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
        public void PublishBundle()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
           

            var result = (ObjectResult)controller.PublishBundle(-2, 100).Result;
            var bundle = (ObjectResult)controller.Get(-2).Result;
            var bundleDto = bundle.Value as BundleDto;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(t => t.Name == bundleDto.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(Bundle.BundleStatus.Published);


        }
        
        [Fact]
        public void AddTour() { 
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            List<TourDto> tours = new List<TourDto>();
            var tourDto = new TourDto
            {
                Id = 1,
                Name = "ime",
                Description = "naziv",
                Status = API.Dtos.AccountStatus.PUBLISHED, // Assumption: 1 corresponds to Published status in your code
                Difficulty = 1, // Assumption: 1 corresponds to the Difficulty value in your code
                Price = 100.0,
                Tags = new List<string> { "Prva vrednost", "Druga vrednost" }, // Adjust as needed
                Equipment = new List<int>(),
                CheckPoints = new List<long>(),
                AuthorId = -11,
                Objects = new List<long>(), // Add missing field
                FootTime = 1, // Add missing field
                BicycleTime = 1, // Add missing field
                CarTime = 1, // Add missing field
                TotalLength = 1, // Add missing field
                PublishTime = new DateTime(2023, 1, 1, 13, 0, 0, DateTimeKind.Utc), // Add missing field               
            };
            tours.Add(tourDto);
            var bundle = new BundleDto
            {
                Id = -1,
                UserId = -11,
                Name = "Novi",
                Price = 0,
                Status = BundleStatus.Draft,
                Tours = new List<int>()
            };

            // Act
            var result = (ObjectResult)controller.AddTourToBundle(bundle,tourDto.Id).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Id == bundle.Id);
            storedEntity.ShouldNotBeNull();
        }


        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Bundles.FirstOrDefault(i => i.Id == 1);
            storedCourse.ShouldBeNull();
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
