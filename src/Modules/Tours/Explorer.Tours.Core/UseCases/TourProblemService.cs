using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System.Linq;

namespace Explorer.Tours.Core.UseCases;

public class TourProblemService : CrudService<TourProblemDto, TourProblem>, ITourProblemService
{
    private readonly ITourService _tourService;

    public TourProblemService(ICrudRepository<TourProblem> repository, IMapper mapper, ITourService tourService) : base(repository, mapper)
    {
        _tourService = tourService;
    }


    public Result<PagedResult<TourProblemDto>> GetByTouristId(int touristId, int page, int pageSize)
    {
        try
        {
            var pagedResult = GetPaged(page, pageSize);
            if(pagedResult != null){

                var filteredTourProblems = pagedResult.Value.Results.Where(tp => tp.TouristId == touristId).ToList();

                var filteredTourProblemsPagedResult = new PagedResult<TourProblemDto>(
                    filteredTourProblems,
                    filteredTourProblems.Count
                );

                return Result.Ok(filteredTourProblemsPagedResult);
            }
            return Result.Fail("Tour Problem pagedResult is null");
        }
        catch(KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourProblemDto> GetByTourId(int tourId)
    {
        try
        {
            var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
            var tourProblems = pagedResult.Results.Where(tp => tp.TouristId == tourId).FirstOrDefault();

            if (tourProblems != null)
            {
                return MapToDto(tourProblems);
            }
            else
            {
                return Result.Ok().WithSuccess("There are no tour problems reported");

            }
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<PagedResult<TourProblemDto>> GetByAuthorId(int authorId, int page, int pageSize)
    {
        try
        {
            var authorsToursResult = _tourService.GetToursListByAuthor(authorId, page, pageSize);
            var tourProblemsResult = GetPaged(page, pageSize);

            if (authorsToursResult.IsSuccess && tourProblemsResult.IsSuccess)
            {
                var authorTourIds = authorsToursResult.Value.Select(authorTour => authorTour.Id);

                var filteredTourProblems = tourProblemsResult.Value.Results.Where(tp => authorTourIds.Contains((int)tp.TourId)).ToList();
                
                var filteredTourProblemsPagedResult = new PagedResult<TourProblemDto>(
                    filteredTourProblems,
                    filteredTourProblems.Count
                );

                return Result.Ok(filteredTourProblemsPagedResult);
            }
            if (!authorsToursResult.IsSuccess && !tourProblemsResult.IsSuccess)
            {
                return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);
            }
            else if (authorsToursResult.IsSuccess && !tourProblemsResult.IsSuccess)
            {
                return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);
            }
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail("FailureCode.NotFound, Nije nadjen").WithError(e.Message);
        }
    }

}