using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class CheckpointVisitedService : CrudService<CheckpointVisitedDto, CheckpointVisited>, ICheckpointVisitedService
    {
        public CheckpointVisitedService(ICrudRepository<CheckpointVisited> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
