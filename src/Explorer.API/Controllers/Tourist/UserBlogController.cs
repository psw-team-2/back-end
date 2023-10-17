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
        private readonly IWebHostEnvironment _environment;
        public UserBlogController(IUserBlogService userBlogService, IWebHostEnvironment environment)
        {
            _userBlogService = userBlogService;
            _environment = environment;
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

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "Images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"{file.FileName} successfully uploaded to the Server";
            }
            catch (Exception ex)
            {
                throw;
            }
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
