using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class CheckpointVisitedService : CrudService<CheckpointVisitedDto, CheckpointVisited>, ICheckpointVisitedService
    {
        private readonly ICheckpointVisitedRepository _checkpointVisitedRepository;

        public CheckpointVisitedService(ICrudRepository<CheckpointVisited> repository, ICheckpointVisitedRepository checkpointVisitedRepository,IMapper mapper) : base(
            repository, mapper)
        {
            _checkpointVisitedRepository = checkpointVisitedRepository;
        }

        public Result<PagedResult<CheckpointVisitedDto>> GetVisitedCheckpointsByUser(int userId)
        {
            try
            {
                var visitedCheckpoints = _checkpointVisitedRepository.GetVisitedCheckpointsByUser(userId);
                var visitedCheckPointsDto = MapToDto(visitedCheckpoints).Value;

                var visitedCheckpointsPagedResult = new PagedResult<CheckpointVisitedDto>(
                    visitedCheckPointsDto,
                    visitedCheckPointsDto.Count
                );


                return Result.Ok(visitedCheckpointsPagedResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

        public Result<List<CheckpointVisitedDto>> GetCheckpointsVisitedByIds(List<int> checkpointVisitedIds)
        {
            var checkpointsVisited = _checkpointVisitedRepository.GetCheckpointsVisitedByIds(checkpointVisitedIds);
            var checkpointsVisitedDto = MapToDto(checkpointsVisited).Value;
            return Result.Ok(checkpointsVisitedDto);
        }
    }
}
