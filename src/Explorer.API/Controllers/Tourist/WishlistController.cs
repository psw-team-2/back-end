using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/wishlist")]
    [ApiController]
    public class WishlistController : BaseApiController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService applicationReviewService)
        {
            _wishlistService = applicationReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<WishlistDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _wishlistService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<WishlistDto> GetWishlistByUserId(int userId)
        {
            var result = _wishlistService.GetWishlistByUserId(userId);

            if (result == null)
            {
                return new ObjectResult(new { Message = "Uer don't have a wishlist.", StatusCode = 400 }) { StatusCode = 400 };
            }

            return CreateResponse(result);
        }


        [HttpGet("{id:int}")]
        public ActionResult<WishlistDto> Get(int id)
        {
            var result = _wishlistService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<WishlistDto> Create([FromBody] WishlistDto wishlist)
        {
            var result = _wishlistService.Create(wishlist);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<WishlistDto> Update([FromBody] WishlistDto wishlist)
        {
            var result = _wishlistService.Update(wishlist);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _wishlistService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPost("wishlistItem/{wishlistId:int}/{tourId:int}")]
        public ActionResult<WishlistDto> AddItem([FromBody] WishlistDto wishlist, int tourId)
        {
            var result = _wishlistService.AddItem(wishlist, tourId);
            return CreateResponse(result);
        }

       

        [HttpPut("removeItem/{wishlistId:int}/{itemId:int}")]
        public ActionResult<WishlistDto> RemoveItem(int wishlistId, int itemId)
        {
            var result = _wishlistService.RemoveItem(wishlistId, itemId);
            return CreateResponse(result);
        }


        [HttpPut("removeAllItems/{wishlistId:int}")]
        public ActionResult<WishlistDto> RemoveAllItems(int wishlistId)
        {
            var result = _wishlistService.RemoveAllItems(wishlistId);
            return CreateResponse(result);
        }

       
    }
}
