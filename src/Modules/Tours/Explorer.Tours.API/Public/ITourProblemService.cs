using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourProblemService
{
    Result<PagedResult<TourProblemDto>> GetPaged(int page, int pageSize);
    Result<TourProblemDto> Get(int id);
    Result<TourProblemDto> Create(TourProblemDto problem);
    Result<TourProblemDto> Update(TourProblemDto problem);
    Result Delete(int id);

    public Result<PagedResult<TourProblemDto>> GetByTouristId(int touristId, int page, int pageSize);

    public Result<PagedResult<TourProblemDto>> GetByAuthorId(int authorId, int page, int pageSize);


}