using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Profile = Explorer.Stakeholders.Core.Domain.Profile;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileService : CrudService<ProfileDto, Profile>, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        public ProfileService(ICrudRepository<Profile> repository, IMapper mapper, IProfileRepository profileRepository) : base(repository, mapper) {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public Result<ProfileDto> GetByUserId(int userId)
        {
            foreach (var profile in _profileRepository.GetAll())
            {
                if (profile.UserId == userId)
                {
                    var profileDto = _mapper.Map<ProfileDto>(profile);

                    return Result.Ok(profileDto);
                }
            }

            return Result.Fail("Profile not found for the given userId.");
        }
    }
}
