using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        account.Password = ToSHA256(account.Password);
        var result = _authenticationService.RegisterTourist(account);
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        credentials.Password = ToSHA256(credentials.Password);
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

    private static string ToSHA256(string s)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

        var sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
}