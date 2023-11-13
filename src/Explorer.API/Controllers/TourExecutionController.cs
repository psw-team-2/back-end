using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
   [Route("api/tourexecution")]
   [ApiController]
    public class TourExecutionController : BaseApiController
    {
            private readonly ITourExecutionService _tourExecutionService;
            public TourExecutionController(ITourExecutionService tourExecutionService)
            {
                _tourExecutionService = tourExecutionService;
            }
            
            [HttpPost("start")]
            public ActionResult<TourExecutionDto> StartTour([FromBody] TourExecutionDto tourDto)
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
            [HttpGet("get/{userId:int}")]
            public ActionResult<TourExecutionDto> GetTourExecution(int userId)
            {
                var result = _tourExecutionService.GetTourExecution(userId);
                return CreateResponse(result);
            }

            [HttpPost("complete/{tourExecutionId:int}")]
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

            [HttpPost("abandon/{tourExecutionId:int}")]
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

        [HttpPut("{id:int}")]
        public ActionResult<CheckPointDto> Update([FromBody] TourExecutionDto tourExecution)
        {
            tourExecution.LastActivity = DateTime.UtcNow;
            var result = _tourExecutionService.Update(tourExecution);
            return CreateResponse(result);
        }
    }
}
