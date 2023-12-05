using System;
using System.Collections.Generic;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using AccountStatus = Explorer.Tours.API.Dtos.AccountStatus;


namespace Explorer.Tours.Tests.Integration.Learning.Assessment
{
    [Collection("Sequential")]
    public class TourAgregateTest : BaseToursIntegrationTest
    {
        public TourAgregateTest(ToursTestFactory factory) : base(factory) { }


        [Theory]
        [MemberData(nameof(TourData))]
        public void Adds_checkpoint_to_tour(int checkpointId, TourDto tour, int expectedStatusCode)
        {
            using var scope = Factory.Services.CreateScope();
            var tourController = CreateTourController(scope);
            var checkpointController = CreateCheckPointController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            
            dbContext.Database.BeginTransaction();

            var checkpoint = new CheckPointDto
            {
                Id = -1001,
                Description = "Description",
                Image = "Image",
                Latitude = 45,
                Longitude = 45,
                Name = "Test Checkpoint",
                IsPublic = true,
            };

            var checkpointResultCreate = (ObjectResult)checkpointController.Create(checkpoint).Result;
            checkpointResultCreate.StatusCode.ShouldBe(200);

            var checkpointResultGet = (ObjectResult)checkpointController.Get(checkpointId).Result;
            checkpointResultGet.StatusCode.ShouldBe(expectedStatusCode);

            var result = (ObjectResult)tourController.AddCheckPoint(tour, checkpointId).Result;

            var resultEntity = result.Value as TourDto;

            result.StatusCode.ShouldBe(200);
            if (expectedStatusCode == 200)
            {
                resultEntity.ShouldNotBe(null);
                resultEntity.CheckPoints.ShouldContain(checkpointId);
            }
        }



        [Theory]
        [MemberData(nameof(TourData))]
        public void Adds_equipment_to_tour(int equipmentId, TourDto tour, int expectedStatusCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateTourController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            dbContext.Database.BeginTransaction();

            var result = (ObjectResult)controller.AddEquipmentToTour(tour, equipmentId).Result;

            var resultEntity = result.Value as TourDto;

            result.StatusCode.ShouldBe(200);
            if (expectedStatusCode == 200)
            {
                resultEntity.ShouldNotBe(null);
                resultEntity.Equipment.ShouldContain(equipmentId);   
            }
        }


        [Theory]
        [MemberData(nameof(TourExecutionData))]
        public void Execute_tour(int checkpointId, TourExecutionDto tourExecution, int expectedStatusCode, int expectedCheckpointStatusCode)
        {
            using var scope = Factory.Services.CreateScope();
            var tourExecutionController = CreateTourExecutionController(scope);
            var checkpointController = CreateCheckPointController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            dbContext.Database.BeginTransaction();

            var checkpointResultGet = (ObjectResult)checkpointController.Get(checkpointId).Result;
            checkpointResultGet.StatusCode.ShouldBe(expectedCheckpointStatusCode);


            var result = (ObjectResult)tourExecutionController.StartTour(tourExecution).Result;

            var resultEntity = result.Value as TourDto;

            result.StatusCode.ShouldBe(expectedStatusCode);
            if (expectedStatusCode == 200)
            {
                resultEntity.ShouldNotBe(null);
                resultEntity.Equipment.ShouldContain(checkpointId);
            }
        }

        [Theory]
        [MemberData(nameof(TourData))]
        public void Adds_object_to_tour(int tourObjectId, TourDto tour, int expectedStatusCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateTourController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            dbContext.Database.BeginTransaction();

            var result = (ObjectResult)controller.AddObjectToTour(tour, tourObjectId).Result;

            var resultEntity = result.Value as TourDto;

            result.StatusCode.ShouldBe(200);
            if (expectedStatusCode == 200)
            {
                resultEntity.ShouldNotBe(null);
                resultEntity.Objects.ShouldContain(tourObjectId);
            }
        }




        public static IEnumerable<object[]> TourData()
        {
            yield return new object[]
            {
                -1,
                new TourDto
                {
                    Id = -1,
                    Name = "Tour One",
                    Description = "Description One",
                    Status = AccountStatus.PUBLISHED,
                    Difficulty = 3,
                    Price = 49.99,
                    Tags = new List<string> { "Tag1", "Tag2" },
                    Equipment = new List<int> { -1, -2 },
                    CheckPoints = new List<long> { -1, -2 },
                    Objects = new List<long> { -1, -2 },
                    FootTime = 5.5,
                    BicycleTime = 8.2,
                    CarTime = 3.9,
                    TotalLength = 20.5,
                    PublishTime = DateTime.UtcNow,
                    AuthorId = 123
                },
                200
            };

            yield return new object[]
            {
                -2,
                new TourDto
                {
                    Id = -2,
                    Name = "Tour Two",
                    Description = "Description Two",
                    Status = AccountStatus.DRAFT,
                    Difficulty = 2,
                    Price = 29.99,
                    Tags = new List<string> { "Tag3", "Tag4" },
                    Equipment = new List<int> { -21, -22 },
                    CheckPoints = new List<long> { -21, -22 },
                    Objects = new List<long> { -21, -22 },
                    FootTime = 4.2,
                    BicycleTime = 6.7,
                    CarTime = 2.1,
                    TotalLength = 15.8,
                    PublishTime = DateTime.UtcNow,
                    AuthorId = 456
                },
                404
            };
        }

        public static IEnumerable<object[]> TourExecutionData()
        {
            yield return new object[]
            {
                -1,
                new TourExecutionDto
                {
                    Id = -1,
                    TouristId = -1,
                    TourId = -1,
                    StartTime = DateTime.UtcNow,
                    EndTime = null,
                    Completed = false,
                    Abandoned = false,
                    CurrentLatitude = 40.7128,
                    CurrentLongitude = -74.0060,
                    VisitedCheckpoints = new List<int> { -1, -2, -3 },
                    LastActivity = DateTime.UtcNow,
                    TouristDistance = 15.6
                },
                200, 
                200
            };

            yield return new object[]
            {
                -2,
                new TourExecutionDto
                {
                    Id = -2,
                    TouristId = -2,
                    TourId = -2,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(2),
                    Completed = true,
                    Abandoned = false,
                    CurrentLatitude = 34.0522,
                    CurrentLongitude = -118.2437,
                    VisitedCheckpoints = new List<int> { -1, -2 },
                    LastActivity = DateTime.UtcNow,
                    TouristDistance = 20.3
                },
                200,
                404
            };

            yield return new object[]
            {
                -1,
                new TourExecutionDto
                {

                },
                400,
                200
            };
        }

        private static EquipmentController CreateEquipmentController(IServiceScope scope)
        {
            return new EquipmentController(scope.ServiceProvider.GetRequiredService<IEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static TourController CreateTourController(IServiceScope scope)
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

        private static TourExecutionController CreateTourExecutionController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>(), scope.ServiceProvider.GetRequiredService<ISecretService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}