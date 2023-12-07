using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class CheckPointService : CrudService<CheckPointDto, CheckPoint>, ICheckPointService
{

    private readonly ITourService _tourService;
    private readonly ICheckpointRepository _checkpointRepository;
    private readonly ICheckpointVisitedService _checkpointVisitedService;

    public CheckPointService(ICrudRepository<CheckPoint> repository, ICheckpointVisitedService checkpointVisitedService,IMapper mapper) : base(repository, mapper)
    {
        _checkpointVisitedService = checkpointVisitedService;

    }



    public Result<PagedResult<CheckPointDto>> GetByIds(List<int> checkpointIds, int page, int pageSize)
    {
        var checkpoints = _checkpointRepository.GetCheckpointsByIdsPaged(checkpointIds, page, pageSize);
        var checkpointsDtos = MapToDto(checkpoints);
        
        return checkpointsDtos;
    }

    public Result<PagedResult<CheckPointDto>> GetByTourIdPaged(int tourId, int page, int pageSize)
    {
        try
        {
            //CURRENTLY NOT FUNCTIONAL DUE TO CHECKPOINT ONG ARRAY AND REPO INT

            // TourDto tour = _tourService.Get(tourId).Value;
           // var checkpoints = _checkpointRepository.GetCheckpointsByIdsPaged(tour.Equipment, page, pageSize);
           // var checkpointsDtos = MapToDto(checkpoints);

           // return checkpointsDtos;
            return Result.Fail("Checkpoints pagedResult is null");
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<CheckPointDto> GetCheckpointByCheckpointVisited(int checkpointVisitedId)
    {
        try
        {
            var checkpointVisited = _checkpointVisitedService.Get(checkpointVisitedId).Value;
            var checkpoint = Get((int)checkpointVisited.CheckpointId);
            return checkpoint;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.InvalidArgument);
        }
    }

    public Result<PagedResult<CheckPointDto>> GetCheckPointByCheckpointVisitedIds(List<int> checkpointVisitedIds)
    {
        try
        {

            var checkpointsVisited = _checkpointVisitedService.GetCheckpointsVisitedByIds(checkpointVisitedIds).Value;

            var checkpoints = new List<CheckPointDto>();

            foreach (var checkpointVisited in checkpointsVisited)
            {
                var checkpoint = Get((int)checkpointVisited.CheckpointId).Value; 
                checkpoints.Add(checkpoint);
            }

            var pagedResult = new PagedResult<CheckPointDto>(
                checkpoints,
                checkpoints.Count
            );

            return Result.Ok(pagedResult);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.InvalidArgument);
        }
    }
}