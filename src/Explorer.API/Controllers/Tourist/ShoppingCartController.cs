using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/shoppingCart")]
    [ApiController]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService applicationReviewService)
        {
            _shoppingCartService = applicationReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ShoppingCartDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingCartService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<ShoppingCartDto> GetShoppingCartByUserId(int userId)
        {
            var result = _shoppingCartService.GetShoppingCartByUserId(userId);

            if (result == null)
            {
                return new ObjectResult(new { Message = "Uer don't have a cart.", StatusCode = 400 }) { StatusCode = 400 };
            }

            return CreateResponse(result);
        }


        [HttpGet("{id:int}")]
        public ActionResult<ShoppingCartDto> Get(int id)
        {
            var result = _shoppingCartService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Create(shoppingCart);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Update(shoppingCart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPost("shoppingItem/{shoppingCartId:int}/{tourId:int}")]
        public ActionResult<ShoppingCartDto> AddItem(int shoppingCartId, int tourId,[FromBody] double newPrice)
        {

            var result = _shoppingCartService.AddItem(shoppingCartId, tourId,newPrice);
            return CreateResponse(result);
        }

        [HttpPost("bundleItem/{shoppingCartId:int}/{bundleId:int}")]
        public ActionResult<ShoppingCartDto> AddBundleItem([FromBody] ShoppingCartDto shoppingCart, int bundleId)
        {
            var result = _shoppingCartService.AddBundleItem(shoppingCart, bundleId);
            return CreateResponse(result);
        }

        [HttpPut("removeItem/{shoppingCartId:int}/{itemId:int}")]
        public ActionResult<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId)
        {
            var result = _shoppingCartService.RemoveItem(shoppingCartId, itemId);
            return CreateResponse(result);
        }


        [HttpPut("removeAllItems/{shoppingCartId:int}")]
        public ActionResult<ShoppingCartDto> RemoveAllItems(int shoppingCartId)
        {
            var result = _shoppingCartService.RemoveAllItems(shoppingCartId);
            return CreateResponse(result);
        }

        [HttpGet("totalPrice/{userId:int}")]
        public ActionResult<double> GetTotalPriceByUserId(int userId)
        {
            var result = _shoppingCartService.GetTotalPriceByUserId(userId);
            return CreateResponse(result);
        }

    }
}
