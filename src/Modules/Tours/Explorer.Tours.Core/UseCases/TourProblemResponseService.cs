using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourProblemResponseService : CrudService<TourProblemResponseDto, TourProblemResponse>, ITourProblemResponseService
    {
        public TourProblemResponseService(ICrudRepository<TourProblemResponse> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TourProblemResponseDto> RespondToProblem(TourProblemResponseDto problemResponse)
        {
            try
            {
                var result = CrudRepository.Create(MapToDomain(problemResponse));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<IEnumerable<TourProblemResponseDto>> GetProblemResponses(int problemId)
        {
            try
            {
                var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
                var responses = pagedResult.Results.Where(r => r.TourProblemId == problemId).ToList();

                if (responses.Count > 0)
                {
                    return Result.Ok(responses.Select(MapToDto));
                }
                else
                {
                    return Result.Ok<IEnumerable<TourProblemResponseDto>>(new List<TourProblemResponseDto>()).WithSuccess("No responses to reported problem");
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail<IEnumerable<TourProblemResponseDto>>(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<IEnumerable<TourProblemResponseDto>> GetTourProblemResponsesForUser(int userId)
        {
            try
            {
                var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
                var responses = pagedResult.Results.Where(r => r.CommenterId != userId).ToList();

                if (responses.Count > 0)
                {
                    return Result.Ok(responses.Select(MapToDto));
                }
                else
                {
                    return Result.Ok<IEnumerable<TourProblemResponseDto>>(new List<TourProblemResponseDto>()).WithSuccess("No responses to reported problem");
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail<IEnumerable<TourProblemResponseDto>>(FailureCode.NotFound).WithError(e.Message);
            }
        }


    }
}
