using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ICheckPointService
{
    Result<PagedResult<CheckPointDto>> GetPaged(int page, int pageSize);
    Result<CheckPointDto> Create(CheckPointDto checkPoint);
    Result<CheckPointDto> Update(CheckPointDto checkPoint);
    Result<CheckPointDto> Get(int id);
    Result Delete(int id);
    public Result<PagedResult<CheckPointDto>> GetByTourIdPaged(int tourId, int page, int pageSize);
    public Result<CheckPointDto> GetCheckpointByCheckpointVisited(int checkpointVisitedId);
    public Result<PagedResult<CheckPointDto>> GetCheckPointByCheckpointVisitedIds(List<int> checkpointVisitedIds);
}