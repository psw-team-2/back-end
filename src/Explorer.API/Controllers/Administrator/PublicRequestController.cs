using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/publicRequest")]
    [ApiController]
    public class PublicRequestController : BaseApiController
    {
        private readonly IPublicRequestService _publicRequestService;

        public PublicRequestController(IPublicRequestService publicRequestService)
        {
            _publicRequestService = publicRequestService;
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<PublicRequestDto> Update([FromBody] PublicRequestDto publicRequest)
        {
            var result = _publicRequestService.Update(publicRequest);
            return CreateResponse(result);
        }
    }
}
