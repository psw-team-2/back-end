using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
    public class TourExecutionController : BaseApiController
    {
            private readonly ITourExecutionService _tourExecutionService;

            public TourExecutionController(ITourExecutionService tourExecutionService)
            {
                _tourExecutionService = tourExecutionService;
            }

            [HttpPost("start")]
            public ActionResult<TourExecutionDto> StartTour([FromBody] TourDto tourDto)
            {
                try
                {
                    var tourExecutionDto = _tourExecutionService.StartTour(tourDto);
                    return Ok(tourExecutionDto);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Greška pri pokretanju ture: {ex.Message}");
                }
            }

            [HttpPost("complete/{tourExecutionId}")]
            public ActionResult CompleteTour(int tourExecutionId)
            {
                try
                {
                    _tourExecutionService.CompleteTour(tourExecutionId);
                    return Ok("Tura je uspešno završena.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Greška pri završetku ture: {ex.Message}");
                }
            }

            [HttpPost("abandon/{tourExecutionId}")]
            public ActionResult AbandonTour(int tourExecutionId)
            {
                try
                {
                    _tourExecutionService.AbandonTour(tourExecutionId);
                    return Ok("Tura je napuštena.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Greška pri napuštanju ture: {ex.Message}");
                }
            }
    }
}
