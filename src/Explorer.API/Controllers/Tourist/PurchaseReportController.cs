using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
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

        [HttpGet("byTourist/{touristId}")]
        public ActionResult<PurchaseReportDto> GetPurchaseReportsByTouristId(int touristId)
        {
            var purchaseReportsDto = _purchaseReportService.GetPurchaseReportsByTouristId(touristId);

            if (purchaseReportsDto == null || !purchaseReportsDto.Any())
            {
                return NotFound("No reports found for tourist");
            }

            var result = Result.Ok(purchaseReportsDto);

            return CreateResponse(result);
        }
    }
}
