using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IShoppingCartService 
    {
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<ShoppingCartDto> Get(int id);
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
        public Result<ShoppingCartDto> AddItem(int shoppingCartId, int tourId,double newPrice);
        public Result<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId);
        public Result<ShoppingCartDto> GetShoppingCartByUserId(int userId);
        public Result<ShoppingCartDto> RemoveAllItems(int shoppingCartId);
        public Result<double> GetTotalPriceByUserId(int userId);
        Result<String> CreateTourPurchaseToken(List<OrderItemDto> orderItems, int userId, double dicount);
        Result<ShoppingCartDto> AddBundleItem(ShoppingCartDto shoppingCart, int bundleId);

    }
}
