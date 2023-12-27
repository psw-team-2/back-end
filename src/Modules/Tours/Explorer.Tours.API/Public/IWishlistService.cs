using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IWishlistService
    {
        Result<PagedResult<WishlistDto>> GetPaged(int page, int pageSize);
        Result<WishlistDto> Get(int id);
        Result<WishlistDto> Create(WishlistDto wishlist);
        Result<WishlistDto> Update(WishlistDto wishlist);
        Result Delete(int id);
        public Result<WishlistDto> AddItem(WishlistDto wishlist, int tourId);
        public Result<WishlistDto> RemoveItem(int wishlistId, int itemId);
        public Result<WishlistDto> GetWishlistByUserId(int userId);
        public Result<WishlistDto> RemoveAllItems(int wishlistId);
        bool DoesFavouriteItemExistForTour(int wishlistId, int tourId);


    }
}
