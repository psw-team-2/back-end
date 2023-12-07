using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Create(equipment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentService.Delete(id);
            return CreateResponse(result);
        }
        
        [HttpGet("getTouristSelected/{id:int}")]
        public ActionResult<PagedResult<EquipmentDto>> GetAllTouristSelectedEquipment(int id)
        {
            var result = _equipmentService.GetAllForSelection(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CheckPointDto> Get(long id)
        {
            var result = _equipmentService.Get(id);
            return CreateResponse(result);
        }
        [HttpGet("by-tour/{id:int}")]
        public ActionResult<PagedResult<EquipmentDto>> GetByTourId(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetByTourId(id, page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("by-ids")]
        public ActionResult<PagedResult<EquipmentDto>> GetByIds([FromBody] List<int> equipmentIds, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetByIds(equipmentIds, page, pageSize);
            return CreateResponse(result);
        }

    }
}
