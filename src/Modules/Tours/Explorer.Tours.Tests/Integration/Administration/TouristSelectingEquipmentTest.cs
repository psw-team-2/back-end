using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class TouristSelectingEquipmentTest : BaseToursIntegrationTest
{
    public TouristSelectingEquipmentTest(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TouristSelectedEquipmentDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }
    
    [Fact]
    public void EquipmentSelection_Success()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var equipmentSelectionDto = new TouristSelectedEquipmentDto
        { };

        // Act
        var result = (ObjectResult)controller.EquipmentSelection(equipmentSelectionDto).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
        var returnedObject = result.Value;
        returnedObject.ShouldNotBeNull();
        returnedObject.ShouldBeAssignableTo<TouristSelectedEquipmentDto>();
        var touristSelectedEquipmentDto = (TouristSelectedEquipmentDto)returnedObject;
    }

    private static TouristSelectedEquipmentController CreateController(IServiceScope scope)
    {
        return new TouristSelectedEquipmentController(scope.ServiceProvider.GetRequiredService<ITouristSelectedEquipmentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}

