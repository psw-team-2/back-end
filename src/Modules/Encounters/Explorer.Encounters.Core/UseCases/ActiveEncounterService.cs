using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class ActiveEncounterService : CrudService<ActiveEncounterDto, ActiveEncounter>, IActiveEncounterService
    {
        private readonly IActiveEncounterRepository _activeEncounterRepository;

        public ActiveEncounterService(ICrudRepository<ActiveEncounter> repository, IMapper mapper, IActiveEncounterRepository activeEncounterRepository) : base(repository, mapper)
        {
            activeEncounterRepository = activeEncounterRepository;
        }

        public Result<PagedResult<ActiveEncounterDto>> GetAllByEncounterId(int id, int page, int pageSize)
        {
            try
            {
                var pagedResult = GetPaged(page, pageSize);
                if (pagedResult != null)
                {

                    var filteredActiveEncounters = pagedResult.Value.Results.Where(ae => ae.EncounterId == id).ToList();

                    var filteredActiveEncountersPagedResult = new PagedResult<ActiveEncounterDto>(
                        filteredActiveEncounters,
                        filteredActiveEncounters.Count
                    );

                    return Result.Ok(filteredActiveEncountersPagedResult);
                }
                return Result.Fail("Tour Problem pagedResult is null");
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}
