using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ICrudRepository<TourPurchaseToken> _tourPurchaseTokenRepository;
        private readonly ICrudRepository<OrderItem> _orderItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper, ICrudRepository<OrderItem> orderItemRepository, IShoppingCartRepository shoppingCartRepository) : base(repository, mapper)
        {
            _tourPurchaseTokenRepository = repository;
            _orderItemRepository = orderItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public Result<TourPurchaseTokenDto> CreateTourPurchaseToken(List<OrderItemDto> orderItems, int userId)
        {
            ShoppingCart shoppingCart = _shoppingCartRepository.GetShoppingCartByUserId(userId);
            foreach (OrderItemDto item in orderItems)
            {
                TourPurchaseToken purchaseToken = new TourPurchaseToken(userId, item.TourId, DateTime.UtcNow);
                _tourPurchaseTokenRepository.Create(purchaseToken);
                shoppingCart.RemoveItem(item.Id);
            }    
            shoppingCart.TotalPrice = 0;
            _shoppingCartRepository.Update(shoppingCart);

            return Result.Ok();
        }


    }
}
