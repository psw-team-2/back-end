using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserAccountAdministrationService _userAccountAdministrationService;

    public AuthenticationController(IAuthenticationService authenticationService, IUserAccountAdministrationService userAccountAdministrationService)
    {
        _authenticationService = authenticationService;
        _userAccountAdministrationService = userAccountAdministrationService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CredentialsDto> GetUsernameById(int id)
    {
        var result = _authenticationService.GetUsername(id);
        return CreateResponse(result);
    }

    [HttpGet("userids")]
    public ActionResult<List<long>> GetAllUserIds()
    {
        var result = _authenticationService.GetAllUserIds();
        return CreateResponse(result);
    }

    [HttpGet("{userId}")]
    public ActionResult<UserAccountDto> GetUserById(long userId)
    {
        var result = _authenticationService.GetUserById(userId);

        if (result.IsFailed)
        {
            // Handle the failure, e.g., return a 404 (Not Found) response or appropriate error response.
            return NotFound();
        }

        return Ok(result.Value); // Return the user information as a successful response.
    }

    [HttpGet("whole/{userId}")]
    public ActionResult<UserAccountDto> GetWholeUserById(long userId)
    {
        var result = _authenticationService.GetWholeUserById(userId);

        if (result.IsFailed)
        {
            // Handle the failure, e.g., return a 404 (Not Found) response or appropriate error response.
            return NotFound();
        }

        return Ok(result.Value); // Return the user information as a successful response.
    }

    [HttpPut("updateUser/{id:int}/{role:int}")]
    public ActionResult<UserAccountDto> UpdateUser(int id, int role, [FromBody] UserAccountDto userAccountDto)
    {
        userAccountDto.Id = id;
        if (role == 0)
        {
            userAccountDto.Role = Stakeholders.API.Dtos.UserRole.Administrator;
        }
        else if (role == 1)
        {
            userAccountDto.Role = Stakeholders.API.Dtos.UserRole.Author;
        }
        else
        {
            userAccountDto.Role = Stakeholders.API.Dtos.UserRole.Tourist;
        }

        var result = _userAccountAdministrationService.Update(userAccountDto);
        return CreateResponse(result);
    }

}