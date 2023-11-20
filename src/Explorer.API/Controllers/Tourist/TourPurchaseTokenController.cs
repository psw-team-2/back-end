using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/tourPurchaseToken")]
    [ApiController]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;

        public TourPurchaseTokenController(ITourPurchaseTokenService tourPurchaseTokenService)
        {
            _tourPurchaseTokenService = tourPurchaseTokenService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPurchaseTokenDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPurchaseTokenService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("createTokens/{userId}")]
        public ActionResult CreateTourPurchaseToken([FromBody] List<OrderItemDto> orderItems, [FromRoute] int userId)
        {
            var result = _tourPurchaseTokenService.CreateTourPurchaseToken(orderItems, userId);
            return CreateResponse(result);          
        }



    }
}
