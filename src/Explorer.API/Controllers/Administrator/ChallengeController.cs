using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/administrator/challenge")]
    [ApiController]
    public class ChallengeController : BaseApiController
    {
        private readonly IChallengeService _challengeService;
        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ChallengeDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _challengeService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ChallengeDto> Get(int id)
        {
            var result = _challengeService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ChallengeDto> Create([FromBody] ChallengeDto challenge)
        {
            var result = _challengeService.Create(challenge);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<ChallengeDto> Update([FromBody] ChallengeDto challenge)
        {
            var result = _challengeService.Update(challenge);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _challengeService.Delete(id);
            return CreateResponse(result);
        }
    }
}
