using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface IPublicRequestService
    {
        Result<PagedResult<PublicRequestDto>> GetPaged(int page, int pageSize);
        Result<PublicRequestDto> Create(PublicRequestDto publicRequest);
        Result<PublicRequestDto> Update(PublicRequestDto publicRequest);
        Result Delete(int id);
        Result<IEnumerable<PublicRequestDto>> GetPublicRequestsByUserId(int userId);
    }
}
