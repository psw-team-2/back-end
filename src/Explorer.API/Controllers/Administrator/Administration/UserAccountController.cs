using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
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

        [HttpPut("{id:int}")]
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


    }
}
