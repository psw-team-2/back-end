using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/touristposition")]
    public class CheckpointVisitedController : BaseApiController
    {
        private readonly ICheckpointVisitedService _checkpointVisitedService;
        private readonly IWebHostEnvironment _environment;

        public CheckpointVisitedController(ICheckpointVisitedService checkpointVisitedService, IWebHostEnvironment environment)
        {
            _checkpointVisitedService = checkpointVisitedService;
            _environment = environment;
        }


        [HttpPost]
        public ActionResult<CheckpointVisitedDto> Create([FromBody] CheckpointVisitedDto checkpointVisitedDto)
        {
            var result = _checkpointVisitedService.Create(checkpointVisitedDto);
            return CreateResponse(result);
        }

        [HttpGet("get/{id:int}")]
        public ActionResult<CheckpointVisitedDto> Get(int id)
        {
            var result = _checkpointVisitedService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<CheckpointVisitedDto> Update([FromBody] CheckpointVisitedDto checkpointVisitedDto)
        {
            var result = _checkpointVisitedService.Update(checkpointVisitedDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _checkpointVisitedService.Delete(id);
            return CreateResponse(result);
        }
    }
}
