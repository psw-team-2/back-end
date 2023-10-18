using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize]
    [Route("api/administration/tour-problems")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;

        public TourProblemController(ITourProblemService tourProblemService)
        {
            _tourProblemService = tourProblemService;
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "tourist,administrator")]
        public ActionResult<TourProblemDto> Create([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Create(tourProblem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "administrator")]
        public ActionResult<EquipmentDto> Update([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Update(tourProblem);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "administrator")]
        public ActionResult Delete(int id)
        {
            var result = _tourProblemService.Delete(id);
            return CreateResponse(result);
        }
    }
}