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
    public class TouristPositionController : BaseApiController
    {
        private readonly ITouristPositionService _touristPositionService;
        private readonly IWebHostEnvironment _environment;

        public TouristPositionController(ITouristPositionService touristPositionService, IWebHostEnvironment environment)
        {
            _touristPositionService = touristPositionService;
            _environment = environment;
        }


        [HttpPost]
        public ActionResult<TouristPositionDto> Create([FromBody] TouristPositionDto touristPositionDto)
        {
            var result = _touristPositionService.Create(touristPositionDto);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TouristPositionDto> Get(int id)
        {
            var result = _touristPositionService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TouristPositionDto> Update([FromBody] TouristPositionDto touristPositionDto)
        {
            var result = _touristPositionService.Update(touristPositionDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristPositionService.Delete(id);
            return CreateResponse(result);
        }
    }
}
