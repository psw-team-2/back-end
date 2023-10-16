using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/applicationReview")]
    [ApiController]
    public class ApplicationReviewController : BaseApiController
    {
        private readonly IApplicationReviewService _applicationReviewService;

        public ApplicationReviewController(IApplicationReviewService applicationReviewService)
        {
            _applicationReviewService = applicationReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ApplicationReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _applicationReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ApplicationReviewDto> Create([FromBody] ApplicationReviewDto applicationReview)
        {
            var result = _applicationReviewService.Create(applicationReview);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ApplicationReviewDto> Update([FromBody] ApplicationReviewDto applicationReview)
        {
            var result = _applicationReviewService.Update(applicationReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _applicationReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
