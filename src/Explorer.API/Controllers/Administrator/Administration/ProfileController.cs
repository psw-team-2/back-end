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
        private readonly IWebHostEnvironment _environment;

        public ProfileController(IProfileService profileService, IWebHostEnvironment environment)
        {
            _profileService = profileService;
            _environment = environment;
        }

        
        [HttpGet("{id:int}")]
        public ActionResult<ProfileDto> Get(int id)
        {
            var result = _profileService.Get(id);
            return CreateResponse(result);
        }
        

        [HttpGet("{userId:int}")]
        public ActionResult<ProfileDto> GetByUserId(int userid)
        {
            var result = _profileService.GetByUserId(userid);
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
