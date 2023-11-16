using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public interface ITokenGenerator
{
    Result<AuthenticationTokensDto> GenerateAccessToken(User user, long personId);
}