using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IChallengeService
    {
        Result<PagedResult<ChallengeDto>> GetPaged(int page, int pageSize);
        Result<ChallengeDto> Get(int id);
        Result<ChallengeDto> Create(ChallengeDto challengeDto);
        Result<ChallengeDto> Update(ChallengeDto challengeDto);
        Result Delete(int id);
    }
}
