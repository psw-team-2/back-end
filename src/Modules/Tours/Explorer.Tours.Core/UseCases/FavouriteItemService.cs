using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class FavouriteItemService : CrudService<FavouriteItemDto, FavouriteItem>, IFavouriteItemService
    {

        private readonly IFavouriteItemRepository _favouriteItemRepository;

        public FavouriteItemService(ICrudRepository<FavouriteItem> repository, IMapper mapper, IFavouriteItemRepository favouriteItemRepository) : base(repository, mapper)
        {
            _favouriteItemRepository = favouriteItemRepository;
        }

        

        public Result<IEnumerable<FavouriteItemDto>> GetFavouriteItemsByWishlist(int wishlistId)
        {
            IEnumerable<FavouriteItem> favouriteItems = _favouriteItemRepository.GetFavouriteItemsByWishlist(wishlistId);
            List<FavouriteItemDto> dtosForItems = new List<FavouriteItemDto>();
            foreach (var item in favouriteItems)
            {
                
                    FavouriteItemDto dto = new FavouriteItemDto
                    {
                        Id = (int)item.Id,
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        Price = item.Price,
                        WishlistId = wishlistId,
                        
                    };
                    dtosForItems.Add(dto);
                

            }

            return dtosForItems;
        }


    
}
}
