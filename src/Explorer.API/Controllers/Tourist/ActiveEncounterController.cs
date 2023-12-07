using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/activeEncounter")]
    [ApiController]
    public class ActiveEncounterController : BaseApiController
    {
        private readonly IActiveEncounterService _activeEncounterService;
        public ActiveEncounterController(IActiveEncounterService activeEncounterService)
        {
            _activeEncounterService = activeEncounterService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ActiveEncounterDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _activeEncounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ActiveEncounterDto> Get(int id)
        {
            var result = _activeEncounterService.Get(id);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<ActiveEncounterDto> Create([FromBody] ActiveEncounterDto challenge)
        {
            var result = _activeEncounterService.Create(challenge);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<ActiveEncounterDto> Update([FromBody] ActiveEncounterDto challenge)
        {
            var result = _activeEncounterService.Update(challenge);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _activeEncounterService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("byencounter/{id:int}")]
        public ActionResult<PagedResult<ActiveEncounterDto>> GetAllById(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _activeEncounterService.GetAllByEncounterId(id, page, pageSize);
            return CreateResponse(result);
        }

    }
}
