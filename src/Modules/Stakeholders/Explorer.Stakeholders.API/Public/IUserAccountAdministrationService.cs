using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserAccountAdministrationService
    {
        Result<PagedResult<UserAccountDto>> GetPaged(int page, int pageSize);
        Result<UserAccountDto> Update(UserAccountDto equipment);
        Result Delete(int id);
        public Result<UserAccountDto> GetUserAccountById(long userId);
    }
}
