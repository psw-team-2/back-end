using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserAccountAdministrationService : CrudService<UserAccountDto, User>, IUserAccountAdministrationService
    {
        private readonly IUserRepository _userRepository;

        public UserAccountAdministrationService(IUserRepository userRepository, ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) {
            _userRepository = userRepository;
        }

        /*
        public Result<UserDto> Get(int id)
        {
            return _userRepository.Get(id);
        }
        */

        /*
        public Result AddTourPreference(TourPreferenceDto tourPreferenceDto)
        {
            TourPreference tourPreference = new TourPreference(tourPreferenceDto.Difficulty, tourPreferenceDto.WalkingRating, tourPreferenceDto.BicycleRating, tourPreferenceDto.CarRating, tourPreferenceDto.BoatRating, tourPreferenceDto.Tags);

            User user = _userRepository.Get((int)tourPreferenceDto.TouristId);
            user.AddTourPreference(tourPreference);

            _userRepository.Update(user);


            return Result.Ok();

        }
        */
    }
}
