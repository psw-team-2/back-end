using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/favouriteItem")]
    
    public class FavouriteItemController : BaseApiController
    {
        private readonly IFavouriteItemService _favouriteItemService;

        public FavouriteItemController(IFavouriteItemService favouriteItemService)
        {
            _favouriteItemService = favouriteItemService;
        }

        [HttpGet("favouriteItems/{userId}")]
        public ActionResult<PagedResult<FavouriteItemDto>> GetFavouriteItemsByWishlist(int userId)
        {
            var result = _favouriteItemService.GetFavouriteItemsByWishlist(userId);
            return CreateResponse(result);
        }

        [HttpPut("update/{id:int}")]
        public ActionResult<FavouriteItemDto> Update([FromBody] FavouriteItemDto favouriteItem)
        {
            var result = _favouriteItemService.Update(favouriteItem);
            return CreateResponse(result);
        }
    }
}
