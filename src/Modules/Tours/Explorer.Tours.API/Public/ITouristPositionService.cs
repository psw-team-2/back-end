using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface ITouristPositionService
    {
        Result<TouristPositionDto> Create(TouristPositionDto checkPoint);
        Result<TouristPositionDto> Update(TouristPositionDto checkPoint);
        Result<TouristPositionDto> Get(int id);
        Result Delete(int id);
    }
}
