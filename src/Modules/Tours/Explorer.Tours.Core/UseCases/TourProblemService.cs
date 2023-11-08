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

    private Result<TourProblemDto> GetTourProblemById(int id)
    {
        try
        {
            var problem = CrudRepository.Get(id);

            if (problem == null)
            {
                return Result.Fail<TourProblemDto>(FailureCode.NotFound).WithError("Problem not found");
            }

            var problemDto = MapToDto(problem);
            return Result.Ok(problemDto);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private Result<TourProblemDto> GetExistingProblem(int problemId)
    {
        var existingProblemResult = GetTourProblemById(problemId);
        if (existingProblemResult.IsSuccess)
        {
            return existingProblemResult;
        }
        else
        {
            return Result.Fail(FailureCode.NotFound).WithError("Problem not found");
        }
    }

    public Result<TourProblemDto> ChangeProblemStatus(TourProblemDto problem) 
    {
        try
        {
            var existingProblemResult = GetExistingProblem(problem.Id);

            if (existingProblemResult.IsSuccess)
            {
                var existingProblem = existingProblemResult.Value;

                UpdateTourProblem(existingProblem);

                return Result.Ok<TourProblemDto>(problem);
            }
            else
            {
                return Result.Fail(FailureCode.Internal).WithError("Error occurred.");
            }
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private void UpdateTourProblem(TourProblemDto tourProblem)
    {
        tourProblem.IsResolved = true;
        Update(tourProblem);
    }
}