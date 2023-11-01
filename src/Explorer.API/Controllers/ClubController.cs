using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/clubs")]

    [ApiController]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly IWebHostEnvironment _environment;

        public ClubController(IClubService clubService, IWebHostEnvironment environment)
        {
            _clubService = clubService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<ClubDto>> GetClubById(int id)
        {
            var result = _clubService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto club)
        {
            var id = HttpContext.User.Claims.First(x => x.Type == "id");
            long longValue;
            long.TryParse(id.Value, out longValue);
            club.OwnerId = longValue;
            var result = _clubService.Create(club);
            return CreateResponse(result);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }

        [HttpPut("kick/{id:int}")]
        public ActionResult<ClubDto> Kick([FromBody] ClubDto club)
        {
            var id = HttpContext.User.Claims.First(x => x.Type == "id");
            long longValue;
            long.TryParse(id.Value, out longValue);
            club.OwnerId = longValue;
            var result = _clubService.Update(club);
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
    }
}
