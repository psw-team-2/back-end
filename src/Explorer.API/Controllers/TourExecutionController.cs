using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
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
            private readonly ISecretService _secretService;

            public TourExecutionController(ITourExecutionService tourExecutionService, ISecretService secretService)
            {
                _tourExecutionService = tourExecutionService;
                _secretService = secretService;
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
                    return BadRequest($"Bad request: {ex.Message}");
                }
            }
            [HttpGet("get/{userId:int}")]
            public ActionResult<TourExecutionDto> GetTourExecution(int userId)
            {
                var result = _tourExecutionService.GetTourExecution(userId);
                return CreateResponse(result);
            }

            [HttpGet("getSecret/{cpId:int}")]
            public ActionResult<SecretDto> GetSecretForCheckPoint(int cpId)
            {
                var result = _secretService.GetSecretForCheckPoint(cpId);
                return CreateResponse(result);
            }

        /*[HttpPost("complete/{tourExecutionId:int}")]
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
        }*/

        [HttpPut("{id:int}")]
            public ActionResult<TourExecutionDto> Update([FromBody] TourExecutionDto tourExecution)
            {
                tourExecution.LastActivity = DateTime.UtcNow;
                var result = _tourExecutionService.Update(tourExecution);
                return CreateResponse(result);
            }

            [HttpPut("checkpointComplete/{tourExecutionId:int}")]
            public ActionResult<TourExecutionDto> CompleteCheckpoint( int tourExecutionId, [FromBody] List<CheckPointDto> checkpoints)
            {
                var result = _tourExecutionService.CompleteCheckpoint(tourExecutionId, checkpoints);
                return CreateResponse(result);
            }
    }
}
