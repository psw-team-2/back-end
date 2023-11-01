using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Components;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.UseCases;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Microsoft.AspNetCore.Mvc.Route("api/administration/follow")]
    public class FollowController : BaseApiController
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]
        public ActionResult<FollowDto> Create([FromBody] FollowDto follow)
        {
            var result = _followService.Create(follow);
            return CreateResponse(result);
        }

        [HttpGet("all-follows")]
        public ActionResult<PagedResult<FollowDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _followService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
