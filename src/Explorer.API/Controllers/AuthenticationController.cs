﻿using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;

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
}