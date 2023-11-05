using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/comment")]
    
    public class BlogCommentController : BaseApiController
    {
        private readonly IBlogCommentService _blogCommentService;
        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogCommentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto comment)
        {
            var result = _blogCommentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogCommentDto> Update([FromBody] BlogCommentDto comment)
        {
            var result = _blogCommentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogCommentService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("byBlog/{blogId}")]
        public ActionResult<PagedResult<BlogCommentDto>> GetCommentsByBlogId(int blogId)
        {
            var reviewsDto = _blogCommentService.GetCommentsByBlogId(blogId);

            if (reviewsDto == null || !reviewsDto.Any())
            {
                return NotFound("No reviews found for the specified tour ID.");
            }

            return Ok(reviewsDto);
        }
    }
}
