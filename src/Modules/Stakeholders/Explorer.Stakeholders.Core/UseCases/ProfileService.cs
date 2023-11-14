using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using Profile = Explorer.Stakeholders.Core.Domain.Users.Profile;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileService : CrudService<ProfileDto, Profile>, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IFollowRepository _followRepository;
        public ProfileService(ICrudRepository<Profile> repository, IMapper mapper, IProfileRepository profileRepository, IFollowRepository followRepository) : base(repository, mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _followRepository = followRepository;
        }
    }
}
