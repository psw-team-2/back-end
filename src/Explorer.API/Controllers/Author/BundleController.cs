using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/bundle")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;
        private readonly IWebHostEnvironment _environment;
        public BundleController(IBundleService bundleService, IWebHostEnvironment environment)
        {
            _bundleService = bundleService;
            _environment = environment;
        }

        [HttpGet("{id:int}")]
        public ActionResult<BundleDto> Get(int id)
        {
            var result = _bundleService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        
        public ActionResult<PagedResult<BundleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _bundleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BundleDto> Create([FromBody] BundleDto bundle)
        {
            var result = _bundleService.Create(bundle);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BundleDto> Update([FromBody] BundleDto bundle)
        {
            var result = _bundleService.Update(bundle);
            return CreateResponse(result);
        }

        [HttpGet("byAuthor/{userId}")]
        public ActionResult<PagedResult<BundleDto>> GetBundlesByAuthorId(int userId)
        {
            var bundlesDto = _bundleService.GetBundlesByAuthorId(userId);

            if (bundlesDto == null || !bundlesDto.Any())
            {
                return NotFound("No bundles found for author");
            }

            return Ok(bundlesDto);
        }

        [HttpPut("publish/{bundleId}")] 
        public ActionResult<BundleDto> PublishBundle(int bundleId)
        {
            var result = _bundleService.PublishBundle(bundleId);
            return CreateResponse(result);
        }

        [HttpPut("finish-creating/{bundleId}/{price}")]
        public ActionResult<BundleDto> FinishCreatingBundle(int bundleId, double price)
        {
            var result = _bundleService.FinishCreatingBundle(bundleId, price);
            return CreateResponse(result);
        }

        [HttpPost("addTour/{tourId:int}")]
        public ActionResult<BundleDto> AddTourToBundle([FromBody] BundleDto bundle, int tourId)
        {
            var result = _bundleService.AddTour(bundle, tourId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _bundleService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPut("archive/{bundleId}")]
        public ActionResult<BundleDto> ArchiveBundle(int bundleId)
        {
            var result = _bundleService.ArchiveBundle(bundleId);
            return CreateResponse(result);
        }

        [HttpPut("removeTour/{bundleId:int}/{tourId:int}")]
        public ActionResult<BundleDto> RemoveTourFromBundle(int bundleId, int tourId)
        {
            var result = _bundleService.RemoveTour(bundleId, tourId);
            return CreateResponse(result);
        }


        [HttpPost("uploadBundleImage")]
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
    }
}
