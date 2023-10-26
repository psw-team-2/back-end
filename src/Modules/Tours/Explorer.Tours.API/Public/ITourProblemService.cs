using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourProblemService
{
    Result<PagedResult<TourProblemDto>> GetPaged(int page, int pageSize);
    Result<TourProblemDto> Create(TourProblemDto problem);
    Result<TourProblemDto> Update(TourProblemDto problem);
    Result Delete(int id);
}