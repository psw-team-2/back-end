using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
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
    [Collection("Sequential")]
    public class TourReviewQueryTests : BaseToursIntegrationTest
    {
        public TourReviewQueryTests(ToursTestFactory factory) : base(factory) { } //ToursTestFactory sluzi da se postavi testna baza podataka, zamenjuje interakciju sa pravom bazom

        [Fact] //anotacija koja govori nasem sistemu da je u pitanju automatski trst
        public void Retrieves_all()
        {   //Arrange - pripremamo sve sto nam treba za test
            using var scope = Factory.Services.CreateScope(); 
            var controller = CreateController(scope);

            //Act - izvrsavamo metodu koju testiramo
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourReviewDto>;

            //Assert - proveravamo rezultat ispitane metode
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);

        }

        private static TourReviewController CreateController(IServiceScope scope)
        {
            var environment = new Mock<IWebHostEnvironment>().Object; // You might need to create a mock environment for testing.

            return new TourReviewController(
                scope.ServiceProvider.GetRequiredService<ITourReviewService>(),
                environment)
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
