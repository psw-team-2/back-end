using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ICheckPointService
{
    Result<PagedResult<CheckPointDto>> GetPaged(int page, int pageSize);
    Result<CheckPointDto> Create(CheckPointDto checkPoint);
    Result<CheckPointDto> Update(CheckPointDto checkPoint);
    Result Delete(int id);
}