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
                Id = 1,
                UserId = 1,
                Name = "Novii",
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
        public void AddTour()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            List<TourDto> tours = new List<TourDto>();
            var tourDto1 = new TourDto
            {
                Id = 1,
                Name = "ime",
                Description = "naziv",
                Status = API.Dtos.AccountStatus.PUBLISHED, // Assumption: 1 corresponds to Published status in your code
                Difficulty = 1, // Assumption: 1 corresponds to the Difficulty value in your code
                Price = 100.0,
                Tags = new List<string> { }, // Adjust as needed
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
            var tourDto2 = new TourDto
            {
                Id = 2,
                Name = "ime",
                Description = "naziv",
                Status = API.Dtos.AccountStatus.PUBLISHED,
                Difficulty = 1,
                Price = 100.0,
                Tags = new List<string> { },
                Equipment = new List<int>(),
                CheckPoints = new List<long>(),
                AuthorId = 1,
                Objects = new List<long>(),
                FootTime = 1,
                BicycleTime = 1,
                CarTime = 1,
                TotalLength = 1,
                PublishTime = new DateTime(2023, 1, 1, 13, 0, 0, DateTimeKind.Utc),
            };
            tours.Add(tourDto1);
            tours.Add(tourDto2);
            var bundle = new BundleDto
            {
                Id = 1,
                UserId = 1,
                Name = "Novii",
                Price = 0,
                Status = BundleStatus.Draft,
                Tours = new List<int>()
            };

            
            // Act
            var result1 = (ObjectResult)controller.AddTourToBundle(bundle, tourDto1.Id).Result;
            var result2 = (ObjectResult)controller.AddTourToBundle(bundle, tourDto2.Id).Result;

            // Assert - Response
            result1.ShouldNotBeNull();
            result1.StatusCode.ShouldBe(200);
            result2.ShouldNotBeNull();
            result2.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Id == bundle.Id);
            storedEntity.ShouldNotBeNull();
        }


        [Fact]
        public void PublishBundle()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = (ObjectResult)controller.PublishBundle(1).Result;
            var bundle = (ObjectResult)controller.Get(1).Result;
            var bundleDto = bundle.Value as BundleDto;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(t => t.Name == bundleDto.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(Bundle.BundleStatus.Published);

        }
        
       
        [Fact]
        public void RemoveTour()
        {
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
                Status = API.Dtos.AccountStatus.PUBLISHED, 
                Difficulty = 1, 
                Price = 100.0,
                Tags = new List<string> { }, 
                Equipment = new List<int>(),
                CheckPoints = new List<long>(),
                AuthorId = 1,
                Objects = new List<long>(),
                FootTime = 1, 
                BicycleTime = 1, 
                CarTime = 1, 
                TotalLength = 1, 
                PublishTime = new DateTime(2023, 1, 1, 13, 0, 0, DateTimeKind.Utc),               
            };         
            tours.Remove(tourDto);
            var bundle = new BundleDto
            {
                Id = 1,
                UserId = 1,
                Name = "Novi",
                Price = 0,
                Status = BundleStatus.Draft,
                Tours = new List<int>()
            };

            // Act
            var result = (ObjectResult)controller.RemoveTourFromBundle(bundle.Id, tourDto.Id).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Id == bundle.Id);

            // Check if the tour has been removed from the bundle's Tours list
            storedEntity.Tours.ShouldNotContain(tourDto.Id);
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
