using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface IMockTourService
{
    Result<PagedResult<MockTourDto>> GetPaged(int page, int pageSize);
    Result<MockTourDto> Create(MockTourDto mockTour);
    Result<MockTourDto> Update(MockTourDto mockTour);
    Result Delete(int id);
}