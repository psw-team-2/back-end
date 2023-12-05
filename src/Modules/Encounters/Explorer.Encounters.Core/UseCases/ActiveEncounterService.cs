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

    }
}
