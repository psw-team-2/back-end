using Explorer.API.Controllers.Administrator.Administration;
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

namespace Explorer.Stakeholders.Tests.Integration.TourProblem
{
    [Collection("Sequential")]
    public class MockTourQueryTests : BaseToursIntegrationTest
    {
        public MockTourQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<MockTourDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3); 
            result.TotalCount.ShouldBe(3);    
        }

        private static MockTourController CreateController(IServiceScope scope)
        {
            return new MockTourController(scope.ServiceProvider.GetRequiredService<IMockTourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}