using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize]
    [Route("api/administration/authorRequest")]
    public class AuthorRequestController : BaseApiController
    {
        private readonly IAuthorRequestService _authorRequestService;

        public AuthorRequestController(IAuthorRequestService authorRequestService)
        {
            _authorRequestService = authorRequestService;
        }



        [HttpPost]
        public ActionResult<AuthorRequestDto> Create([FromBody] AuthorRequestDto authorRequest)
        {
            var result = _authorRequestService.Create(authorRequest);
            return CreateResponse(result);
        }

        [HttpGet("all-author-request")]
        public ActionResult<PagedResult<AuthorRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _authorRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/{profileId:int}")]
        public ActionResult<AuthorRequestDto> Update(int id, int profileId, [FromBody] AuthorRequestDto authorRequest)
        {
            authorRequest.Id = id;
            authorRequest.ProfileId = profileId;

            var result = _authorRequestService.Update(authorRequest);
            return CreateResponse(result);
        }
    }
}
