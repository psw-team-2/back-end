using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/tourPurchaseToken")]
    [ApiController]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;
        private readonly IShoppingCartService _shoppingCartService;

        public TourPurchaseTokenController(ITourPurchaseTokenService tourPurchaseTokenService, IShoppingCartService shoppingCartService)
        {
            _tourPurchaseTokenService = tourPurchaseTokenService;
            _shoppingCartService = shoppingCartService;
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
            var result = _shoppingCartService.CreateTourPurchaseToken(orderItems, userId);
            return CreateResponse(result);          
        }

        [HttpGet("by-tour/{tourId}")]
        public ActionResult GetByTourId(int tourId)
        {
            var result = _tourPurchaseTokenService.GetTourPurchaseTokensByTourId(tourId);
            return CreateResponse(result);
        }

        [HttpGet("by-tour-weekly/{tourId}")]
        public ActionResult GetWeeklyByTourId(int tourId)
        {
            var result = _tourPurchaseTokenService.GetWeeklyTourPurchaseTokensByTourId(tourId);
            return CreateResponse(result);
        }


    }
}
