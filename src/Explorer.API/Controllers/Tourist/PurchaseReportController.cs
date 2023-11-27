using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/purchaseReport")]
    [ApiController]
    public class PurchaseReportController : BaseApiController
    {
        private readonly IPurchaseReportService _purchaseReportService;
        public PurchaseReportController(IPurchaseReportService purchaseReportService)
        {
            _purchaseReportService = purchaseReportService;
        }


        [HttpPost]
        public ActionResult Create([FromBody] List<OrderItemDto> orderItems, [FromRoute] int userId)
        {
            var result = _purchaseReportService.Create(orderItems, userId);
            return CreateResponse(result);
        }
    }
}
