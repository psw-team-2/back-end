﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/equipment")]
    public class EquipmentControllerAuthor : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentControllerAuthor(IEquipmentService equipmentService)
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
    }
}
