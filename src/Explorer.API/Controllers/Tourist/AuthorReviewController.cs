using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/authorReview")]
   // [Authorize(Policy = "touristPolicy")]
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
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<AuthorReviewDto> Create([FromBody] AuthorReviewDto authorReview, int touristId)
        {

            var result = _authorReviewService.Create(authorReview, touristId);
            return CreateResponse(result);
        }

        [HttpGet("{authorId:int}")]
        public ActionResult<PagedResult<AuthorReviewDto>> GetAuthorReviews(int authorId)
        {
            var result = _authorReviewService.GetAuthorReviews(authorId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/disapproved")]
        public ActionResult<AuthorReviewDto> DisapproveAuthorReview(long id)
        {
            var result = _authorReviewService.DisapproveAuthorReview(id);
            return CreateResponse(result);
        }
    }
}
