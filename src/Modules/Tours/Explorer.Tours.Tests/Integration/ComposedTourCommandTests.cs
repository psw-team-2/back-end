using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class ComposedTourCommandTests : BaseToursIntegrationTest
    {
        public ComposedTourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var newTourComposition = new ComposedTourDto
            {
                Id = -17,
                Name = "New Tour",
                Description = "Description of the new tour",
                ToursId = new List<int> { 1, 2 },
                Status = AccountStatus.DRAFT,
                Difficulty = 3,
                Tags = new List<string>{ "Adventure", "Hiking" },
                Equipment = new List<int> { 1, 2 },
                CheckPoints = new List<long> { 123, 456 },
                Objects = new List<long> { 1, 2 },
                PublishTime = DateTime.SpecifyKind(DateTime.Parse("2023-10-22 9:22:00"), DateTimeKind.Utc),
                TotalLength = 0,
                CarTime=0,
                BicycleTime=0,
                FootTime=0,
                AuthorId = -11,
            };

            // Act
            var result = ((ObjectResult)controller.CreateTourComposition(newTourComposition).Result).Value as ComposedTourDto;


            // Assert - Response
            result.ShouldNotBeNull();
            //result.Id.ShouldNotBe(0);

        }

        [Fact]
        public void RetrivesAllUserTours()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var userId = -2;

            // Act
            var result = ((ObjectResult)controller.RetrivesAllUserTours(userId, 0, 0).Result).Value as  PagedResult<TourDto> ;


            // Assert - Response
            result.ShouldNotBeNull();
            //result.Id.ShouldNotBe(0);

        }

        private static ComposedTourController CreateController(IServiceScope scope)
        {
            return new ComposedTourController(scope.ServiceProvider.GetRequiredService<ITourService>(),scope.ServiceProvider.GetRequiredService<IComposedTourService>());
        }

    }

}

