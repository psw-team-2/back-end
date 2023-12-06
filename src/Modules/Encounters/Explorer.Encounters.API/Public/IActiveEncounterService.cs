using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IActiveEncounterService
    {
        Result<PagedResult<ActiveEncounterDto>> GetPaged(int page, int pageSize);
        Result<ActiveEncounterDto> Get(int id);
        Result<ActiveEncounterDto> Create(ActiveEncounterDto activeEncounterDto);
        Result<ActiveEncounterDto> Update(ActiveEncounterDto activeEncounterDto);
        Result Delete(int id);
        Result<ActiveEncounterDto> GetActiveEncounterById(long id);
        Result<ActiveEncounterDto> CompleteEncounter(ActiveEncounterDto activeEncounter);
    }
}
