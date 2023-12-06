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
            _activeEncounterRepository = activeEncounterRepository;
        }

        public Result<ActiveEncounterDto> GetActiveEncounterById(long id)
        {
            try
            {
                var activeEncounter = CrudRepository.Get(id);

                if (activeEncounter == null)
                {
                    return Result.Fail<ActiveEncounterDto>(FailureCode.NotFound).WithError("Active encounter not found");
                }

                var activeEncounterDto = MapToDto(activeEncounter);
                return Result.Ok(activeEncounterDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ActiveEncounterDto> CompleteEncounter(ActiveEncounterDto activeEncounter) 
        {
            try
            {
                var existingActiveEncounterResult = GetActiveEncounterById(activeEncounter.Id);
                if (!existingActiveEncounterResult.IsSuccess)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Active encounter not found.");
                }
                var existingActiveEncounter = existingActiveEncounterResult.Value;

                existingActiveEncounter.State = (API.Dtos.State)State.Done;
                existingActiveEncounter.End = DateTime.Now;
                existingActiveEncounter.XP += 10;

                Update(existingActiveEncounter);

                return Result.Ok(existingActiveEncounter);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
