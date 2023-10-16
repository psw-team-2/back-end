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
    [Route("api/tourist/blog")]
    public class UserBlogController : BaseApiController
    {
        private readonly IUserBlogService _userBlogService;
        public UserBlogController(IUserBlogService userBlogService)
        {
            _userBlogService = userBlogService;
        }

        [HttpGet]
        public ActionResult<PagedResult<UserBlogDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _userBlogService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<UserBlogDto> Create([FromBody] UserBlogDto blog)
        {
            var result = _userBlogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserBlogDto> Update([FromBody] UserBlogDto blog)
        {
            var result = _userBlogService.Update(blog);
            return CreateResponse(result);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _userBlogService.Delete(id);
            return CreateResponse(result);
        }
    }
}
