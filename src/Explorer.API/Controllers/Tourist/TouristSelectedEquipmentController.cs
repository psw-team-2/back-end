using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/[controller]")]
    [ApiController]
    public class TouristSelectedEquipmentController : BaseApiController
    {

        private readonly ITouristSelectedEquipmentService _touristSelectedEquipmentService;

        public TouristSelectedEquipmentController(ITouristSelectedEquipmentService touristSelectedEquipmentService)
        {
            _touristSelectedEquipmentService = touristSelectedEquipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristSelectedEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristSelectedEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristSelectedEquipmentDto> EquipmentSelection([FromBody] TouristSelectedEquipmentDto tourReview)
        {
            var result = _touristSelectedEquipmentService.EquipmentSelection(tourReview);
            return CreateResponse(result);
        }
    }
}

