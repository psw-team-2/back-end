using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize]
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/tour-problem")]
    public class TourProblemAuthorController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;
        private readonly ITourProblemResponseService _problemResponseService;

        public TourProblemAuthorController(ITourProblemService tourProblemService, ITourProblemResponseService problemResponseService)
        {
            _tourProblemService = tourProblemService;
            _problemResponseService = problemResponseService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> Get(int id)
        {
            var result = _tourProblemService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TourProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemService.GetPaged(page, pageSize);
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

        [HttpPost("{id}/respond")]
        public ActionResult RespondToProblem(int id, [FromBody] TourProblemResponseDto tourProblemResponse)
        {
            var result = _problemResponseService.Create(tourProblemResponse);
            return CreateResponse(result); 
        }

        [HttpGet("{problemId:int}/responses")]
        public ActionResult<IEnumerable<TourProblemResponseDto>> GetProblemResponses(int problemId)
        {
            var result = _problemResponseService.GetProblemResponses(problemId);
            return CreateResponse(result);
        }

    }
}