using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourProblemResponseService
    {
        Result<PagedResult<TourProblemResponseDto>> GetPaged(int page, int pageSize);
        Result<TourProblemResponseDto> Create(TourProblemResponseDto problemResponse);
        Result<TourProblemResponseDto> Update(TourProblemResponseDto problemResponse);
        Result Delete(int id);
        Result<TourProblemResponseDto> RespondToProblem(TourProblemResponseDto problemResponse);
        Result<IEnumerable<TourProblemResponseDto>> GetProblemResponses(int problemId);
    }
}
