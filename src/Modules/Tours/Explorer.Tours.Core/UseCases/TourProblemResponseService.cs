using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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

        public Result<TourProblemResponseDto> RespondToProblem(int problemId, string response, int userId)
        {
            try
            {
                var newResponse = new TourProblemResponse(response, DateTime.Now, problemId, userId);
                CrudRepository.Create(newResponse);

                var responseDto = MapToDto(newResponse);

                return Result.Ok(responseDto).WithSuccess("Response added successfully.");
            }
            catch (Exception ex)
            {
                return Result.Fail<TourProblemResponseDto>(FailureCode.Internal).WithError(ex.Message);
            }
        }
    }
}
