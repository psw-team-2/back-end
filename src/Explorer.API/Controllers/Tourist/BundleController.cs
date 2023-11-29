using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/bundle")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<BundleDto> Get(int id)
        {
            var result = _bundleService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        [Authorize(Roles = "author")]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
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
            var reviewsDto = _bundleService.GetBundlesByAuthorId(userId);

            if (reviewsDto == null || !reviewsDto.Any())
            {
                return NotFound("No bundles found for author");
            }

            return Ok(reviewsDto);
        }

    }
}
