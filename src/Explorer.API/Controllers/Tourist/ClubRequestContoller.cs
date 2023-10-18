using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/clubRequests")]
    public class ClubRequestContoller: BaseApiController
    {
        private readonly IClubRequestService _clubRequestService;

        public ClubRequestContoller(IClubRequestService clubRequestService)
        {
            _clubRequestService = clubRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubRequestDto> SendRequest([FromBody] ClubRequestDto clubRequest)
        {
            var result = _clubRequestService.SendRequest(clubRequest);
            return CreateResponse(result);
        }
    }
}
