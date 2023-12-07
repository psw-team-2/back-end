using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using FluentResults;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.API.Controllers.Tourist;


[Route("api/tourist/orderItem")]
public class OrderItemController : BaseApiController
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet("orderItems/{userId}")]
    public ActionResult<PagedResult<OrderItemDto>> GetOrderItemsByShoppingCart(int userId)
    {
        var result = _orderItemService.GetOrderItemsByShoppingCart(userId);
        return CreateResponse(result);
    }

    [HttpPut("update/{id:int}")]
    public ActionResult<OrderItemDto> Update([FromBody] OrderItemDto orderItem)
    {
        var result = _orderItemService.Update(orderItem);
        return CreateResponse(result);
    }
}
