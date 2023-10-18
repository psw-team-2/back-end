using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/mock-tours")]
    public class MockTourController : BaseApiController
    {
        private readonly IMockTourService _mockTourService;

        public MockTourController(IMockTourService mockTourService)
        {
            _mockTourService = mockTourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<MockTourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _mockTourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<MockTourDto> Create([FromBody] MockTourDto mockTourService)
        {
            var result = _mockTourService.Create(mockTourService);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<MockTourDto> Update([FromBody] MockTourDto mockTourService)
        {
            var result = _mockTourService.Update(mockTourService);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _mockTourService.Delete(id);
            return CreateResponse(result);
        }
    }
}