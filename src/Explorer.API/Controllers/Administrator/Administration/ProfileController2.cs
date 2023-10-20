using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/profile2")]
    public class ProfileController2 : BaseApiController
    {
        private readonly IProfileService _profileService;

        public ProfileController2(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProfileDto> Get(int id)
        {
            var result = _profileService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProfileDto> Create([FromBody] ProfileDto profile)
        {
            var result = _profileService.Create(profile);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/{userId:int}")]
        public ActionResult<ProfileDto> Update(int id, int userId, [FromBody] ProfileDto profile)
        {
            profile.Id = id;
            profile.UserId = userId;

            var result = _profileService.Update(profile);
            return CreateResponse(result);
        }
    }
}
