using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/tourReview")]
    [ApiController]
    public class TourReviewController : BaseApiController
    {
        private readonly ITourReviewService _tourReviewService;

        private readonly IWebHostEnvironment _environment;

        private readonly ITourService _tourService;
        public TourReviewController(ITourReviewService tourReviewService, IWebHostEnvironment environment, ITourService tourService)
        {
            _tourReviewService = tourReviewService;
            _environment = environment;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("{userId}")]
        public ActionResult<TourReviewDto> Create([FromBody] TourReviewDto tourReview, int userId)
        {
           
            var result = _tourReviewService.Create(tourReview, userId);
           

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
        public ActionResult<TourReviewDto> Update([FromBody] TourReviewDto tourReview)
        {
            var result = _tourReviewService.Update(tourReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourReviewService.Delete(id);
            return CreateResponse(result);
        }
        /*
        [HttpGet("average-grade/{tourId:int}")]
        public ActionResult<double> GetAverageGrade(int tourId)
        {
            double averageGrade = _tourReviewService.GetAverageGradeForTour(tourId);
            return Ok(averageGrade);
        }*/

        [HttpGet("byTour/{tourId}")]
        public ActionResult<PagedResult<TourReviewDto>> GetByTourId(int tourId)
        {
            var reviewsDto = _tourService.GetByTourId(tourId);

            if (reviewsDto == null || !reviewsDto.Any())
            {
                return NotFound("No reviews found for the specified tour ID.");
            }

            return Ok(reviewsDto);
        }









    }
}
