using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.ComponentModel;
using UserRole = Explorer.Stakeholders.Core.Domain.UserRole;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    private readonly ICrudRepository<Profile> _profileRepository;
    private readonly ITourPreferenceService _tourPreferenceService;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator, ICrudRepository<Profile> profileRepository, ITourPreferenceService tourPreferenceService)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _profileRepository = profileRepository;
        _tourPreferenceService = tourPreferenceService;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            var user = _userRepository.Create(new User(account.Username, account.Password, UserRole.Tourist, true, account.Email));
            //var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email));
            var profile = _profileRepository.Create(new Profile(account.Name, account.Surname, account.ProfilePicture, account.Biography, account.Motto, user.Id, true));

            var tourPreference = _tourPreferenceService.Create(
                    new TourPreferenceDto
                    {
                        Id = (int)user.Id,
                        TouristId = (int)user.Id,
                        CarRating = 1,
                        BoatRating = 1,
                        WalkingRating = 1,
                        BicycleRating = 1,
                        Difficulty = 1,
                        Tags = new List<string>()


                    });

            return _tokenGenerator.GenerateAccessToken(user, profile.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }

    public Result<CredentialsDto> GetUsername(int id)
    {
        CredentialsDto dto = new CredentialsDto()
        {
            Username = _userRepository.Get(id).Username,
            Password = string.Empty
        };
        return dto;
    }

    public Result<List<long>> GetAllUserIds()
    {
        try
        {
            var userIDs = _userRepository.GetAllUserIds();
            return Result.Ok(userIDs);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public Result<object> GetUserById(long userId)
    {
        return _userRepository.GetUserById(userId);
    }

}