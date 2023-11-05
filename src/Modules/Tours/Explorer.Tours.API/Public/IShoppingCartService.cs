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
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        Result Delete(int id);
    }
}
