using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface ICheckpointVisitedService
    {
        Result<CheckpointVisitedDto> Create(CheckpointVisitedDto checkPoint);
        Result<CheckpointVisitedDto> Update(CheckpointVisitedDto checkPoint);
        Result<CheckpointVisitedDto> Get(int id);
        Result Delete(int id);
    }
}
