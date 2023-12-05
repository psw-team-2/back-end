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
            var newTourDto = new TourDto
            {
                Id = -1,
                Name = "New Tour",
                Description = "Description of the new tour",
                Status = AccountStatus.DRAFT,
                Difficulty = 3,
                Price = 50.0,
                Tags = { "Adventure" , "Hiking" },
                Equipment = new List<int> { 1, 2 },
                CheckPoints = new List<long> { 123, 456 },
                AuthorId = -11,
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
                Id = -1, // Replace with a valid tour ID from your test data
                Name = "Updated Tour Name",
                Description = "Updated tour description",
                Status = AccountStatus.DRAFT,
                Difficulty = 4,
                Price = 75.0,
                Tags = { "Adventure", "Hiking" },
                Equipment = new List<int> { 3, 4 },
                CheckPoints = new List<long> { 789, 101 },
                AuthorId = -1
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
                Id = -1000, 
                Name = "Update fail TEST",
                Description = "Update fail TEST",
                Status = AccountStatus.DRAFT,
                Difficulty = 0,
                Price = 50.0,
                Tags = { "Adventure", "Hiking" },
                Equipment = new List<int> { },
                CheckPoints = new List<long> { }
            };

            // Act
            var result = (ObjectResult)controller.Update(tourToUpdate).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void DeletesTour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var tourIdToDelete = 1; // Replace with a valid tour ID from your test data

            // Act
            var result = (ObjectResult)controller.Delete(tourIdToDelete);

            // Assert - Response
            result.StatusCode.ShouldBe(204);

            // Assert - Database
            var deletedTour = dbContext.Tour.FirstOrDefault(t => t.Id == tourIdToDelete);
            deletedTour.ShouldBeNull();
        }

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


        [Theory]
        [MemberData(nameof(CheckpointData))]
        public void CreateCheckpoints(CheckPointDto checkPointDto, CheckpointVisitedDto checkpointVisitedDto, EquipmentDto equipmentDto)
        {
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var checkpointController = CreateCheckPointController(scope);
            var checkpointVisitedController = CreateCheckpointVisitedController(scope);
            var equipmentController = CreateEquipmentController(scope);

            checkpointController.Create(checkPointDto);
            checkpointVisitedController.Create(checkpointVisitedDto);
            equipmentController.Create(equipmentDto);



        }



        public static IEnumerable<object[]> CheckpointData()
        {
            yield return new object[]
            {
                new CheckPointDto()
                {
                    Id = -1,
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                    Name = "Checkpoint 1",
                    Description = "Description for Checkpoint 1",
                    Image = "Image URL 1",
                    IsPublic = true
                },
                new CheckpointVisitedDto()
                {
                    Id = -1,
                    CheckpointId = -1,
                    Time = DateTime.UtcNow,
                    userId = -1
                },
                new EquipmentDto()
                {
                    Id = -1,
                    Name = "Tour Blog Test Equipment 1",
                    Description = "Generic Description 1"
                }
            };

            yield return new object[]
            {
                new CheckPointDto()
                {
                    Id = -2,
                    Latitude = 51.5074,
                    Longitude = -0.1278,
                    Name = "Checkpoint 2",
                    Description = "Description for Checkpoint 2",
                    Image = "Image URL 2",
                    IsPublic = true
                },
                new CheckpointVisitedDto()
                {
                    Id = -2,
                    CheckpointId = -2,
                    Time = DateTime.UtcNow,
                    userId = -1
                },
                new EquipmentDto()
                {
                    Id = -2,
                    Name = "Tour Blog Test Equipment 2",
                    Description = "Generic Description 2"
                }
            };

            
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

        private static CheckPointController CreateCheckPointController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new CheckPointController(scope.ServiceProvider.GetRequiredService<ICheckPointService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }


        private static CheckpointVisitedController CreateCheckpointVisitedController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new CheckpointVisitedController(scope.ServiceProvider.GetRequiredService<ICheckpointVisitedService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static EquipmentController CreateEquipmentController(IServiceScope scope)
        {
            return new EquipmentController(scope.ServiceProvider.GetRequiredService<IEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}


