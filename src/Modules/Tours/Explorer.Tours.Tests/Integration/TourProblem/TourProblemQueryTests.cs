using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Explorer.Stakeholders.Tests;
using Explorer.Tours.Tests;
using Xunit;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public;

namespace Explorer.Stakeholders.Tests.Integration.TourProblem
{
    [Collection("Sequential")]
    public class TourProblemQueryTests : BaseToursIntegrationTest
    {
        public TourProblemQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(1, 0, 0).Result)?.Value as PagedResult<TourProblemDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(0);
            result.TotalCount.ShouldBe(0);   
        }

        private static TourProblemTouristController CreateController(IServiceScope scope)
        {
            return new TourProblemTouristController(scope.ServiceProvider.GetRequiredService<ITourProblemService>(), scope.ServiceProvider.GetRequiredService<ITourProblemResponseService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}