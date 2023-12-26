using Explorer.Blog.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/giftCard")]
    public class GiftCardController : BaseApiController
    {
        private readonly IGiftCardService _giftCardService;

        public GiftCardController(IGiftCardService giftCardService)
        {
            _giftCardService = giftCardService;
        }

        [HttpPost]
        public ActionResult<GiftcardDto> Create([FromBody] GiftcardDto giftCard)
        {
            var result = _giftCardService.Create(giftCard);
            return CreateResponse(result);
        }

       
    }
}
