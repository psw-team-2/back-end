using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.UseCases;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize]
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

        [HttpPut]
        public ActionResult<UserAccountDto> Update([FromBody] UserAccountDto user)
        {
            var result = _userAccountAdministrationService.Update(user);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _userAccountAdministrationService.Delete(id);
            return CreateResponse(result);
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
