using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.API.Controllers;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Dtos;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Explorer.Tours.Core.Domain;
using AccountStatus = Explorer.Tours.API.Dtos.AccountStatus;


namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogTourTest : BaseBlogIntegrationTest
    {
        public BlogTourTest(BlogTestFactory factory) : base(factory) { }

        [Theory]
        [MemberData(nameof(BlogTourData))]
        public void Create_blog_with_TourReport(UserBlogDto userBlogDto, int expectedResponse, int expectedResponse2)
        {

            using var scope = Factory.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();


            var userBlogController = CreateUserBlogController(scope);
            

            var userBlogResult = (ObjectResult)userBlogController.CreateWithTourReport(userBlogDto).Result;
           
            var blogResultEntity = userBlogResult.Value;

            blogResultEntity.ShouldNotBeNull();
            userBlogResult.StatusCode.ShouldBe(expectedResponse);
        }

        [Theory]
        [MemberData(nameof(BlogTourData))]
        public void Get_blog_with_TourReport(UserBlogDto userBlogDto, int expectedResponse, int expectedResponse2)
        {
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var userBlogController = CreateUserBlogController(scope);



            var userBlogResult = (ObjectResult)userBlogController.Get(userBlogDto.Id).Result;

            var equipmentResult = (ObjectResult)userBlogController.GetEquipmentByTourReport(userBlogDto.Id).Result;
//            var checkpointResult = (ObjectResult)userBlogController.GetCheckpointsByTourReport(userBlogDto.Id).Result;

            userBlogResult.ShouldNotBeNull();
            equipmentResult.ShouldNotBeNull();
//            checkpointResult.ShouldNotBeNull();

            userBlogResult.StatusCode.ShouldBe(expectedResponse2);
            equipmentResult.StatusCode.ShouldBe(expectedResponse2);
//            checkpointResult.StatusCode.ShouldBe(expectedResponse2);
        }




        public static IEnumerable<object[]> BlogTourData()
        {
            yield return new object[]
            {
                new UserBlogDto()
                {
                    Id = -21,
                    UserId = -1,
                    Username = "Test User",
                    Title = "Test Title",
                    Description = "Test Description",
                    CreationTime = DateTime.UtcNow,
                    Status = BlogStatus.Draft,
                    Image = "Test Image",
                    Category = BlogCategory.Accommodation,
                    TourReport = new UserBlogTourReportDto()
                    {
                        TourId = -1, 
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow,
                        Length = 1,
                        Equipment = new List<int>(){-1,-2},
                        CheckpointsVisited = new List<int>(){-1, -2},
                    }
                },
                200,
                200
            };

            yield return new object[]
            {
                new UserBlogDto()
                {
                    Id = -22,
                    UserId = -2,
                    Username = "Test User 2",
                    Title = "Test Title 2",
                    Description = "Test Description 2",
                    CreationTime = DateTime.UtcNow,
                    Status = BlogStatus.Draft,
                    Image = "Test Image 2",
                    Category = BlogCategory.Destinations,
                    TourReport = new UserBlogTourReportDto()
                    {
                        TourId = -2,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(-1),
                        Length = 2,
                        Equipment = new List<int>(){},
                        CheckpointsVisited = new List<int>(){ },
                    }
                },
                400,
                404
            };

            yield return new object[]
            {
                new UserBlogDto()
                {
                    Id = -23,
                    UserId = -3,
                    Username = "",
                    Title = "",
                    Description = "",
                    CreationTime = DateTime.UtcNow,
                    Status = BlogStatus.Draft,
                    Image = "",
                    Category = BlogCategory.Accommodation,
                    TourReport = new UserBlogTourReportDto()
                    {
                        TourId = -3,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(1),
                        Length = 3,
                        Equipment = new List<int>(){-1, -2, -3 },
                        CheckpointsVisited = new List<int>(){},
                    }
                },
                400,
                404
            };
        }
        

        private static UserBlogController CreateUserBlogController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
            return new UserBlogController(scope.ServiceProvider.GetRequiredService<IUserBlogService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>(), context)
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

        private static TourController CreateTourController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IPublicRequestService>(), environment)
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

        private static CheckpointVisitedController CreateCheckpointVisitedController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<ToursContext>();
            return new CheckpointVisitedController(scope.ServiceProvider.GetRequiredService<ICheckpointVisitedService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        
    }

}


