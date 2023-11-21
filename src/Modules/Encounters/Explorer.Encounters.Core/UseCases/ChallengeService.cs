using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class ChallengeService : CrudService<ChallengeDto, Challenge>, IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(ICrudRepository<Challenge> repository, IMapper mapper, IChallengeRepository challengeRepository) : base(repository, mapper)
        {
            _challengeRepository = challengeRepository;
        }
    }
}
