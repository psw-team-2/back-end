using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize);
        Result<EncounterDto> Get(int id);
        Result<EncounterDto> Create(EncounterDto challengeDto);
        Result<EncounterDto> Update(EncounterDto challengeDto);
        Result Delete(int id);
    }
}
