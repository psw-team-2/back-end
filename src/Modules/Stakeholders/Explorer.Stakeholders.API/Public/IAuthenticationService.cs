using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAuthenticationService
{
    Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account, string token);
    Result<CredentialsDto> GetUsername(int id);
    Result<List<long>> GetAllUserIds();
    Result<object> GetUserById(long userId);
    Result<UserAccountDto> GetUserByEmail(string email);
    Result<PagedResult<UserAccountDto>> GetAuthors();

    //novo
    Result<object> GetWholeUserById(long userId);
}