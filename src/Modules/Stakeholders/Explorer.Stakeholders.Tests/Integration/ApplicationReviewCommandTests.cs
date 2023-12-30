using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;
using Shouldly;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Tests;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Public.Administration;
using Explorer.API.Controllers.Tourist;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class ApplicationReviewCommandTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationReviewCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ApplicationReviewDto
            {
                Grade = 3,
                Comment = "Excellent",
                UserId = 1,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ApplicationReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Grade.ShouldBe(newEntity.Grade);
            result.Comment.ShouldBe(newEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.ApplicationReview.FirstOrDefault(i => i.Grade == newEntity.Grade);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ApplicationReviewDto
            {
                Grade = 0,
                Comment = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }


        [Fact]
        public void Delete_fails_invalid_id()   
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new ApplicationReviewDto
            {
                Id = -2,
                Grade = 2,
                Comment = "Very bad experience",
                UserId = -41,
                TimeStamp = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ApplicationReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Grade.ShouldBe(updatedEntity.Grade);
            result.Comment.ShouldBe(updatedEntity.Comment);
            

            // Assert - Database
            var storedEntity = dbContext.ApplicationReview.FirstOrDefault(i => i.Comment == "Very bad experience");
            storedEntity.ShouldNotBeNull();
            storedEntity.Grade.ShouldBe(updatedEntity.Grade);
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ApplicationReviewDto
            {
                Id = -1000,
                Grade = 4,
                Comment = "Test",
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()   //ne radi
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.ApplicationReview.FirstOrDefault(i => i.Id == 2);
            storedCourse.ShouldBeNull();
        }

        private static ApplicationReviewController CreateController(IServiceScope scope)
        {
            return new ApplicationReviewController(scope.ServiceProvider.GetRequiredService<IApplicationReviewService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
