using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Public.Administration;
using Xunit;
using AccountStatus = Explorer.Tours.API.Dtos.AccountStatus;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newTourDto = new TourDto()
            {
                Id = -53,
                Name = "New Tour",
                Description = "Description of the new tour",
                Status = AccountStatus.DRAFT,
                Difficulty = 3,
                Price = 50.0,
                Tags = new List<string>{ "Adventure" , "Hiking" },
                Equipment = new List<int> { 123, 456 },
                CheckPoints = new List<long> { 123, 456 },
                Objects = new List<long> {123, 456},
                AuthorId = -42,
                FootTime = 1,
                BicycleTime = 1,
                CarTime = 1,
                TotalLength = 1,
                PublishTime = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Create(newTourDto).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newTourDto.Name);

            // Assert - Database
            var storedTour = dbContext.Tour.FirstOrDefault(t => t.Name == newTourDto.Name);
            storedTour.ShouldNotBeNull();
            storedTour.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void CreateTourFailsWithInvalidData()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidTourDto = new TourDto
            {
                Name = "",
                Description = "",
                Status = AccountStatus.DRAFT,
                Difficulty = 0,
                Price = 50.0,
                Tags = {},
                Equipment = new List<int> {  },
                CheckPoints = new List<long> { }
            };

            // Act
            var result = (ObjectResult)controller.Create(invalidTourDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void UpdatesTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourToUpdate = new TourDto
            {
                Id = -53,
                Name = "New Tour Updated",
                Description = "Description of the new tour Updated",
                Status = AccountStatus.ARCHIVED,
                Difficulty = 3,
                Price = 50.0,
                Tags = new List<string> { "Adventure", "Hiking" , "Bowling"},
                Equipment = new List<int> { 123},
                CheckPoints = new List<long> { 123},
                Objects = new List<long> { 123},
                AuthorId = -42,
                FootTime = 5,
                BicycleTime = 5,
                CarTime = 5,
                TotalLength = 5,
                PublishTime = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = ((ObjectResult)controller.Update(tourToUpdate).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(tourToUpdate.Id);
            result.Name.ShouldBe(tourToUpdate.Name);
            result.Description.ShouldBe(tourToUpdate.Description);
            result.Status.ShouldBe(tourToUpdate.Status);
            result.Difficulty.ShouldBe(tourToUpdate.Difficulty);
            result.Price.ShouldBe(tourToUpdate.Price);
            result.Tags.ShouldBe(tourToUpdate.Tags);
            result.Equipment.ShouldBe(tourToUpdate.Equipment); 
            result.CheckPoints.ShouldBe(tourToUpdate.CheckPoints);

            // Assert - Database
            var updatedTour = dbContext.Tour.FirstOrDefault(t => t.Id == tourToUpdate.Id);
            updatedTour.ShouldNotBeNull();
            updatedTour.Name.ShouldBe(tourToUpdate.Name);
        }

        [Fact]
        public void UpdateTourFailsWithInvalidId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tourToUpdate = new TourDto
            {
                Id = -453,
                Name = "New Tour Updated",
                Description = "Description of the new tour Updated",
                Status = AccountStatus.ARCHIVED,
                Difficulty = 3,
                Price = 50.0,
                Tags = new List<string> { "Adventure", "Hiking", "Bowling" },
                Equipment = new List<int> { 123 },
                CheckPoints = new List<long> { 123 },
                Objects = new List<long> { 123 },
                AuthorId = -42,
                FootTime = 5,
                BicycleTime = 5,
                CarTime = 5,
                TotalLength = 5,
                PublishTime = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = (ObjectResult)controller.Update(tourToUpdate).Result;
            var resultEntitity = result.Value;

            // Assert
            resultEntitity.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        /*[Fact]
        public void DeletesTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourIdToDelete = -54; // Replace with a valid tour ID from your test data

            // Act
            var result = (ObjectResult)controller.Delete(tourIdToDelete);

            // Assert - Response
            result.StatusCode.ShouldBe(204);

            // Assert - Database
            var deletedTour = dbContext.Tour.FirstOrDefault(t => t.Id == tourIdToDelete);
            deletedTour.ShouldBeNull();
        }*/

        [Fact]
        public void DeleteTourFailsWithInvalidId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidTourId = -1000; // Use an invalid ID that doesn't exist in the test data

            // Act
            var result = (ObjectResult)controller.Delete(invalidTourId);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static TourController CreateController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IPublicRequestService>(), environment)
            {
                ControllerContext = BuildContext("-1")
            };
        }
        
    }
}


