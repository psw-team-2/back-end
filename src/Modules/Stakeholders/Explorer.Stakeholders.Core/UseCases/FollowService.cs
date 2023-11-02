using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class FollowService : CrudService<FollowDto, Follow>, IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IProfileService _profileService;

        public FollowService(ICrudRepository<Follow> repository, IMapper mapper, IFollowRepository followRepository, IProfileService profileService) : base(repository, mapper)
        {
            _followRepository = followRepository;
            _profileService = profileService;
        }

        public Result<PagedResult<ProfileDto>> GetAllFollowers(int page, int pageSize, long profileId)
        {
            var followers = _followRepository.GetAll()
                .Where(follow => follow.ProfileId == profileId)
                .Select(follow =>
                {
                    var profileResult = _profileService.Get((int)follow.FollowerId);

                    if (profileResult.IsSuccess)
                    {
                        var profile = profileResult.Value;

                        return new ProfileDto
                        {
                            Id = profile.Id,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            ProfilePicture = profile.ProfilePicture,
                            Biography = profile.Biography,
                            Motto = profile.Motto,
                            UserId = profile.UserId,
                            IsActive = profile.IsActive
                        };
                    }

                    return new ProfileDto();
                })
                .ToList();

            var pagedResult = new PagedResult<ProfileDto>(followers, followers.Count());
            return Result.Ok(pagedResult);
        }
    }
}
