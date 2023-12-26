using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/administrator/token")]
    public class TokenController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService, IWebHostEnvironment environment)
        {
            _tokenService = tokenService;
        }

        [HttpGet("{value}")]
        public ActionResult<TokenDto> Get(string value)
        {
            var result = _tokenService.Get(value);
            return CreateResponse(result);
        }

        [HttpPost("{email}")]
        public ActionResult<TokenDto> Create(TokenDto tokenDto, string email)
        {
            var result = _tokenService.Create(tokenDto);
            EmailService.SendRecoveryEmail(email, tokenDto.Value);
            return CreateResponse(result);
        }
    }
}
