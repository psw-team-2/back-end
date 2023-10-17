using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/addcheckpoint/checkpoint")]
    public class CheckPointController : BaseApiController
    {
        private readonly ICheckPointService _checkPointService;

        public CheckPointController(ICheckPointService checkPointService)
        {
            _checkPointService = checkPointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CheckPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CheckPointDto> Create([FromBody] CheckPointDto checkPoint)
        {
            var result = _checkPointService.Create(checkPoint);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CheckPointDto> Update([FromBody] CheckPointDto checkPoint)
        {
            var result = _checkPointService.Update(checkPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _checkPointService.Delete(id);
            return CreateResponse(result);
        }
    }
}
