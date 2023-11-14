using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserAccountAdministrationService
    {
        // USER
        Result<UserAccountDto> Get(int id);
        Result<PagedResult<UserAccountDto>> GetPaged(int page, int pageSize);
        Result<UserAccountDto> Update(UserAccountDto equipment);
        Result Delete(int id);



        // PROFILE
        Result<ProfileDto> GetByUserId(int id);
        Result AddFollow(FollowDto follow);
        Result<PagedResult<ProfileDto>> GetAllFollowers(int page, int pageSize, long profileId);
        Result<PagedResult<ProfileDto>> GetAllFollowing(int page, int pageSize, long profileId);



        // MESSAGE
        Result<PagedResult<MessageDto>> GetUnreadMessages(int page, int pageSize, long profileId);
    }
}
