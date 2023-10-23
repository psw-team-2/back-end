using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/clubRequests")]
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

        [HttpPost("sendRequest")]
        public ActionResult<ClubRequestDto> SendRequest([FromBody] ClubRequestDto clubRequest)
        {
            var result = _clubRequestService.Create(clubRequest);
            return CreateResponse(result);
        }

        [HttpDelete("withdrawRequest/{id:int}")]
        public ActionResult<ClubRequestDto> WithdrawRequest(int id)
        {
            var result = _clubRequestService.Delete(id);
            return CreateResponse(result);
        } 

        [HttpPost("acceptRequest")]
        public ActionResult<ClubRequestDto> AcceptRequest([FromBody] ClubRequestDto clubRequest)
        {
            var result = _clubRequestService.Update(clubRequest);
            return CreateResponse(result);
        }

        [HttpPost("rejectRequest")]
        public ActionResult<ClubRequestDto> RejectRequest([FromBody] ClubRequestDto clubRequest)
        {
            var result = _clubRequestService.Update(clubRequest);
            return CreateResponse(result);
        }

    }
}
