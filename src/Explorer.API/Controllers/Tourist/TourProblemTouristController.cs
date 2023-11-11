using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize]
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-problem")]
    public class TourProblemTouristController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemTouristController(ITourProblemService tourProblemService)
        {
            _tourProblemService = tourProblemService;
        }

        [HttpGet("by-tourist/{touristId:int}")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll(int touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemService.GetByTouristId(touristId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourProblemDto> Create([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Create(tourProblem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Update(tourProblem);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourProblemService.Delete(id);
            return CreateResponse(result);
        }
    }
}