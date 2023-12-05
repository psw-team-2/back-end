using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.Tourist
{
    
    [Route("api/tourComposition")]
    [ApiController]
    public class ComposedTourController : BaseApiController
    {
        IComposedTourService _composedTourService;
        private readonly ITourService _tourService;

        public ComposedTourController(ITourService tourService,IComposedTourService composedTourService)
        {
            _composedTourService = composedTourService;
            _tourService = tourService;
        }

        [HttpPost]
        public ActionResult<ComposedTourDto> CreateTourComposition([FromBody]ComposedTourDto newTourComposition)
        {
            var result = _composedTourService.Create(newTourComposition);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<TourDto>>  RetrivesAllUserTours(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.RetrivesAllUserTours(id,page,pageSize);
            return CreateResponse(result);
        }
    }
}
