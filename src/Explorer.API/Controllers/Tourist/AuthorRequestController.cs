using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.Domain.Users;

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

        [HttpGet("all-author-requests")]
        public ActionResult<PagedResult<AuthorRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _authorRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("all-under-review")]
        public ActionResult<PagedResult<AuthorRequestDto>> GetAllUnderReview([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _authorRequestService.GetAllUnderReview(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/{profileId:int}/{status:int}")]
        public ActionResult<AuthorRequestDto> Update(int id, int profileId, int status, [FromBody] AuthorRequestDto authorRequest)
        {
            authorRequest.Id = id;
            authorRequest.ProfileId = profileId;
            if(status == 0)
            {
                authorRequest.RequestStatus = Stakeholders.API.Dtos.RequestStatus.UnderReview;
            }
            else if(status == 1)
            {
                authorRequest.RequestStatus = Stakeholders.API.Dtos.RequestStatus.Accepted;
            }
            else
            {
                authorRequest.RequestStatus = Stakeholders.API.Dtos.RequestStatus.Declined;
            }


            var result = _authorRequestService.Update(authorRequest);
            return CreateResponse(result);
        }
    }
}
