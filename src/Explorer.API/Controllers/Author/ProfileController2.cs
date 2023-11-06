using Explorer.BuildingBlocks.Core.UseCases;
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
        private readonly IWebHostEnvironment _environment;

        public ProfileController2(IProfileService profileService, IWebHostEnvironment environment)
        {
            _profileService = profileService;
            _environment = environment;
        }

        [HttpGet("by-id/{id:int}")]
        public ActionResult<ProfileDto> GetById(int id)
        {
            var result = _profileService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("by-user")]
        public ActionResult<ProfileDto> GetByUserId()
        {
            var userIdClaim = HttpContext.User.Claims.First(x => x.Type == "id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int id))
            {

                var result = _profileService.GetByUserId(id);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("User ID not found or invalid.");
            }

        }
        /*
        public ActionResult<ProfileDto> GetByUserId(int userId)
        {
            var result = _profileService.GetByUserId(userId);
            return CreateResponse(result);
        }
        */

        [HttpGet("all-profiles")]
        public ActionResult<PagedResult<ProfileDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _profileService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProfileDto> Create([FromBody] ProfileDto profile)
        {
            var result = _profileService.Create(profile);
            return CreateResponse(result);
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "Images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"{file.FileName} successfully uploaded to the Server";
            }
            catch (Exception ex)
            {
                throw;
            }
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
