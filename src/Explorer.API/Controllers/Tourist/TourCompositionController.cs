using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourComposition")]
    [ApiController]
    public class TourCompositionController : BaseApiController
    {
        IComposedTourService _composedTourService;

        public TourCompositionController()
        {
        }

        public TourCompositionController(IComposedTourService composedTourService)
        {
            _composedTourService = composedTourService;
        }

        [HttpPost]
        public ActionResult<TourCompositionDto> CreateTourComposition([FromBody]TourCompositionDto newTourComposition)
        {
            throw new NotImplementedException();
        }
    }
}
