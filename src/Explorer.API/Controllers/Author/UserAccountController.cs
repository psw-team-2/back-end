using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.UseCases;
using System.Text;
using System.Security.Cryptography;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/userAccounts")]
    public class UserAccountController : BaseApiController
    {
        private readonly IUserAccountAdministrationService _userAccountAdministrationService;

        public UserAccountController(IUserAccountAdministrationService userAccountAdministrationService)
        {
            _userAccountAdministrationService = userAccountAdministrationService;
        }

        [HttpGet]
        public ActionResult<PagedResult<UserAccountDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _userAccountAdministrationService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserAccountDto> GetById(int id)
        {
            var result = _userAccountAdministrationService.Get(id);
            return CreateResponse(result);
        }
        [HttpGet("token/{token}")]
        public ActionResult<UserAccountDto> GetByToken(string token)
        {
            var result = _userAccountAdministrationService.GetByToken(token);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserAccountDto> Update([FromBody] UserAccountDto user)
        {
            if (user.Password.Length != 64)
            {
                user.Password = ToSHA256(user.Password);
            }
            var result = _userAccountAdministrationService.Update(user);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _userAccountAdministrationService.Delete(id);
            return CreateResponse(result);
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




        /*
        [HttpGet("tourPreference/byTourist")]
        public ActionResult GetMyPreference(int userId)
        {
            var userIdClaim = HttpContext.User.Claims.First(x => x.Type == "id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int touristId))
            {

                var result = _userAccountAdministrationService.Get(userIdClaim);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("User ID not found or invalid.");
            }

        }
        */

        /*
        [HttpPost("tourPreference/")]
        public ActionResult<TourPreferenceDto> Create([FromBody] TourPreferenceDto tourPreference)
        {
            var result = _tourPreferenceService.Create(tourPreference);
            return CreateResponse(result);
        }
        


        [HttpPut("tourPreference/{id:int}")]
        public ActionResult<TourPreferenceDto> Update([FromBody] TourPreferenceDto tourPreference)
        {
            var result = _tourPreferenceService.Update(tourPreference);
            return CreateResponse(result);
        }

        [HttpDelete("tourPreference/{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPreferenceService.Delete(id);
            return CreateResponse(result);
        }
        

        [HttpGet("tourPreference/")]
        public ActionResult<PagedResult<TourPreferenceDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)

        {
            var userIdClaim = HttpContext.User.Claims.First(x => x.Type == "id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int touristId))
            {

                var result = _tourPreferenceService.GetByTouristId(touristId);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("User ID not found or invalid.");
            }

        }
        */
    }
}
