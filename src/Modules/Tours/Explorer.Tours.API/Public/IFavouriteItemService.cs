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
    public interface IFavouriteItemService
    {
        public Result<IEnumerable<FavouriteItemDto>> GetFavouriteItemsByWishlist(int wishlistId);
        
        Result<FavouriteItemDto> Update(FavouriteItemDto favouriteItem);
    }
}
