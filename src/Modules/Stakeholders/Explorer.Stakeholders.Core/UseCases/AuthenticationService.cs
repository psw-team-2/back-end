using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using System.ComponentModel;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using UserRole = Explorer.Stakeholders.Core.Domain.Users.UserRole;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain;
using System.Xml.XPath;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    private readonly ICrudRepository<Profile> _profileRepository;
    private readonly ITourPreferenceService _tourPreferenceService;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IWalletService _walletService;
    private readonly IWishlistService _wishlistService;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator, ICrudRepository<Profile> profileRepository, IShoppingCartService shoppingCartService, IWalletService walletService, IWishlistService wishlistService)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _profileRepository = profileRepository;
        _shoppingCartService = shoppingCartService;
        _walletService = walletService;
        _wishlistService = wishlistService;
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
    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account, string token)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {

            var user = _userRepository.Create(new User(account.Username, account.Password, UserRole.Tourist, false, account.Email, token));
            //var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email));
            var profile = _profileRepository.Create(new Profile(account.Name, account.Surname, account.ProfilePicture, account.Biography, account.Motto, user.Id, true, 0, false, false));

            //kreiranje korpe
            var shoppingCart = _shoppingCartService.Create(new Payments.API.Dtos.ShoppingCartDto
                    {
                        Id = (int)user.Id,
                        UserId = user.Id,
                        Items = new List<int>(),
                        TotalPrice = 0
                    });

            //kreiranje novcanika
            var wallet = _walletService.Create(new Payments.API.Dtos.WalletDto
            {   Id = 0,
                UserId = (int)user.Id,
                Username = user.Username,
                AC = 0
            });

            //kreiranje wishlista
            var wishlist = _wishlistService.Create(new Tours.API.Dtos.WishlistDto
            {
                Id = 0,
                UserId = user.Id,
                Items = new List<int>(),
                
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

    public Result<UserAccountDto> GetUserByEmail(string email)
    {
        try
        {
            User result = _userRepository.GetByEmail(email);

            if (result != null)
            {
                UserAccountDto dto = new UserAccountDto
                {
                    Id = (int)result.Id,
                    Username = result.Username,
                    Password = result.Password,
                    Email = result.Email,
                    Role = (API.Dtos.UserRole)(int)result.Role,
                    IsActive = result.IsActive
                };

                return Result.Ok(dto);
            }
            else
            {
                return Result.Fail<UserAccountDto>("User not found");
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /*
    public Result DeleteApplicationReviewByUser(ApplicationReviewDto applicationReviewDto)
    {
        ApplicationReview applicationReview = new ApplicationReview(applicationReviewDto.Grade, DateTime.UtcNow, applicationReviewDto.UserId, applicationReviewDto.Comment);

        User user = _userRepository.GetUserById<User>(applicationReviewDto.UserId);
        user.DeleteApplicationReview(applicationReview.Id);

        _userRepository.Update(user.Id);
    }*/

    public Result<PagedResult<UserAccountDto>> GetAuthors()
    {
        try
        {
            var authors = _userRepository.GetAuthors();

            var authorsDto = authors.Select(user => new UserAccountDto
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Role = (API.Dtos.UserRole)user.Role,
                IsActive = user.IsActive
            }).ToList();

            var pagedResult = new PagedResult<UserAccountDto>(authorsDto, authorsDto.Count);

            return Result.Ok(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Authors not found");
        }
    }
}