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

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
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
    }
}
