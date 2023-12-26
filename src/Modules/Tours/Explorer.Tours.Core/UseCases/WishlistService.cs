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
    public class WishlistService : CrudService<WishlistDto, Wishlist>, IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly ICrudRepository<FavouriteItem> _crudFavouriteItemRepository;
        private readonly IFavouriteItemRepository _favouriteItemRepository;
      


        public WishlistService(ICrudRepository<Wishlist> repository, IMapper mapper, IWishlistRepository wishlistRepository, ICrudRepository<Tour> tourRepository, ICrudRepository<FavouriteItem> crudFavouriteItemRepository, IFavouriteItemRepository favouriteItemRepository) : base(repository, mapper)
        {
            _wishlistRepository = wishlistRepository;
            _tourRepository = tourRepository;
            _crudFavouriteItemRepository = crudFavouriteItemRepository;
            _favouriteItemRepository = favouriteItemRepository;
           
        }


        public Result<WishlistDto> AddItem(WishlistDto wishlistDto, int tourId)
        {
            try
            {
                Tours.Core.Domain.Tour tour = _tourRepository.Get(tourId);
                if (wishlistDto != null)
                {
                    FavouriteItem favouriteItem = new FavouriteItem(tourId, tour.Name, tour.Price, wishlistDto.Id);
                    _crudFavouriteItemRepository.Create(favouriteItem);

                    Wishlist wishlist = _wishlistRepository.GetById(wishlistDto.Id);

                    wishlist.AddItem((int)favouriteItem.Id);

                    
                    _wishlistRepository.Update(wishlist);
                    return Result.Ok(wishlistDto);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Wishlist not found.");
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }


        public Result<WishlistDto> GetWishlistByUserId(int userId)
        {

            try
            {
                var wishlist = _wishlistRepository.GetWishlistByUserId(userId);
                
                return MapToDto(wishlist);
            }
            catch (Exception e)
            {
                return Result.Fail<WishlistDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<WishlistDto> RemoveItem(int wishlistId, int itemId)
        {
            try
            {
                Wishlist wishlist = _wishlistRepository.GetById(wishlistId);
                FavouriteItem favouriteItem = GetFavouriteItemById(itemId);
                wishlist.RemoveItem(itemId);

                
                _wishlistRepository.Update(wishlist);
                _crudFavouriteItemRepository.Delete(itemId);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<WishlistDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Result<WishlistDto> RemoveAllItems(int wishlistId)
        {
            try
            {
                Wishlist wishlist = _wishlistRepository.GetById(wishlistId);
                wishlist.RemoveAllItems();
                _favouriteItemRepository.RemoveAllItemsByWishlistId(wishlistId);
                _wishlistRepository.Update(wishlist);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<WishlistDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private FavouriteItem GetFavouriteItemById(int id)
        {
            FavouriteItem favouriteItem = _crudFavouriteItemRepository.Get(id);
            return favouriteItem;
        }

        
        
    }
}
