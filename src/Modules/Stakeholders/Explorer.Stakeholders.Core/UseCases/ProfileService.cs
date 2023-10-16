using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Profile = Explorer.Stakeholders.Core.Domain.Profile;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileService : CrudService<ProfileDto, Profile>, IProfileService
    {
        public ProfileService(ICrudRepository<Profile> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
