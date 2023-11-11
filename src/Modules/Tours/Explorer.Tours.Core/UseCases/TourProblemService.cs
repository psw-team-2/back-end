using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;


namespace Explorer.Tours.Core.UseCases;

public class TourProblemService : CrudService<TourProblemDto, TourProblem>, ITourProblemService
{
    public TourProblemService(ICrudRepository<TourProblem> repository, IMapper mapper) : base(repository, mapper) { }


    public Result<TourProblemDto> GetByTouristId(int touristId)
    {
        try
        {
            var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
            var tourProblems = pagedResult.Results.Where(tp => tp.TouristId == touristId).FirstOrDefault();

            if (tourProblems != null)
            {
                return MapToDto(tourProblems);
            }
            else
            {
                return Result.Ok().WithSuccess("There are no tour problems reported");

            }
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

}