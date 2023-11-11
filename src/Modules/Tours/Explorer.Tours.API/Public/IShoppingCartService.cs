using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IShoppingCartService
    {
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<ShoppingCartDto> Get(int id);
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
        public Result<ShoppingCartDto> AddItem(ShoppingCartDto shoppingCart, int tourId);
        public Result<ShoppingCartDto> RemoveItem(ShoppingCartDto shoppingCart, int itemId);
        public Result<ShoppingCartDto> GetShoppingCartByUserId(int userId);
    }
}
