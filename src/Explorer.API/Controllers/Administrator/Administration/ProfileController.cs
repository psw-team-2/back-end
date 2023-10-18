using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/administration/profile")]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public ActionResult<ProfileDto> Create([FromBody] ProfileDto profile)
        {
            var result = _profileService.Create(profile);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProfileDto> Update(int id /*dodato*/, [FromBody] ProfileDto profile)
        {
            profile.Id = id; // dodato

            var result = _profileService.Update(profile);
            return CreateResponse(result);
        }
    }
}
