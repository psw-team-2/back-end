using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration
{
    public class TourReviewCommandTests : BaseToursIntegrationTest
    {
        public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourReviewDto
            {
                Id = -1,
                Comment = "good",
                Grade = 5,
                UserId = 1,
                ReviewDate = DateTime.SpecifyKind(DateTime.Parse("2023-10-22 9:22:00"), DateTimeKind.Utc),
                VisitDate = DateTime.SpecifyKind(DateTime.Parse("2023-10-22 9:22:00"), DateTimeKind.Utc),
                TourId = 1,
                Images = "slika"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Images.ShouldBe(updatedEntity.Images);
            result.Comment.ShouldBe(updatedEntity.Comment);
            result.Grade.ShouldBe(updatedEntity.Grade);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.VisitDate.ShouldBe(updatedEntity.VisitDate);
            result.ReviewDate.ShouldBe(updatedEntity.ReviewDate);
            result.TourId.ShouldBe(updatedEntity.TourId);

            // Assert - Database
            var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "good");
            storedEntity.ShouldNotBeNull();
            storedEntity.Grade.ShouldBe(updatedEntity.Grade);
            storedEntity.Images.ShouldBe(updatedEntity.Images);
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            storedEntity.VisitDate.ShouldBe(updatedEntity.VisitDate);
            storedEntity.ReviewDate.ShouldBe(updatedEntity.ReviewDate);
            storedEntity.TourId.ShouldBe(updatedEntity.TourId);
            var oldEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "Voda");
            oldEntity.ShouldBeNull();
        }

      
    
    private static TourReviewController CreateController(IServiceScope scope)
        {
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>(), environment, scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
