using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/club")]
    public class ClubMessageController : BaseApiController
    {
        private readonly IClubMessageService _clubMessageService;

        public ClubMessageController(IClubMessageService clubMessageService)
        {
            _clubMessageService = clubMessageService;
        }

        [HttpGet("{clubId:int}/chatroom")]
        public ActionResult<PagedResult<ClubMessageDto>> GetAll(int clubId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubMessageService.GetByClubId(clubId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubMessageDto> Create([FromBody] ClubMessageDto clubMessage)
        {
            var result = _clubMessageService.Create(clubMessage); 
            return CreateResponse(result);
        }
    }
}
