using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/blog")]
    public class UserBlogController : BaseApiController
    {
        private readonly IUserBlogService _userBlogService; 
        private readonly IWebHostEnvironment _environment;
        private readonly BlogContext _context;
        public UserBlogController(IUserBlogService userBlogService, IWebHostEnvironment environment, BlogContext context)
        {
            _userBlogService = userBlogService;
            _environment = environment;
            _context = context;
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
                if (Request.Form.Files.Count > 0)
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
                else
                {
                    
                    return "No files uploaded.";
                }
            }
            catch (Exception ex)
            {
                
                return $"Error: {ex.Message}";
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
        [HttpGet("{id}")]
        public ActionResult<UserBlogDto> Get(int id)
        {
            var blog = _userBlogService.Get(id);
            if (blog == null)
            {
                return NotFound(); 
            }

            return CreateResponse(blog);
        }

        [HttpGet("byUser/{userId}")]
        public ActionResult<PagedResult<UserBlogDto>> GetByUserId(int userId)
        {
            var blogsDtos = _userBlogService.GetByUserId(userId);

            if (blogsDtos == null || !blogsDtos.Any())
            {
                return NotFound("No blogs found for the specified user ID.");
            }

            return Ok(blogsDtos);
        }

        [HttpPut("AddRating")]
        public ActionResult AddRating(RatingDto ratingDto)
        {
            var result = _userBlogService.AddRating(ratingDto);
            return CreateResponse(result);
        }

        [HttpGet("RatingCount")]
        public ActionResult<RatingCount> GetRatingsCount([FromQuery] int blogId)
        {
            var count = _userBlogService.GetRatingsCount(blogId);
            return CreateResponse(count);
        }


    }
}
