using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/authorReview")]
    [Authorize(Policy = "touristPolicy")]
    [ApiController]
    public class AuthorReviewController: BaseApiController
    {
        private readonly IAuthorReviewService _authorReviewService;

        public AuthorReviewController(IAuthorReviewService authorReviewService)
        {
            _authorReviewService = authorReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<AuthorReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _authorReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("{touristId}")]
        public ActionResult<AuthorReviewDto> Create([FromBody] AuthorReviewDto authorReview, int touristId)
        {

            var result = _authorReviewService.Create(authorReview, touristId);
            return CreateResponse(result);
        }


    }
}
