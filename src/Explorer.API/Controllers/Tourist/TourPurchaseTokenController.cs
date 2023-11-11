using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
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
    }
}
