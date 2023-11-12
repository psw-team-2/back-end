using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly ICrudRepository<OrderItem> _orderItemRepository;



        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository shoppingCartRepository, ICrudRepository<Tour> tourRepository, ICrudRepository<OrderItem> orderItemRepository) : base(repository, mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _tourRepository = tourRepository;
            _orderItemRepository = orderItemRepository;
        }


        public Result<ShoppingCartDto> AddItem(ShoppingCartDto shoppingCartDto, int tourId)
        {
            Tour tour = _tourRepository.Get(tourId);
            if (shoppingCartDto != null)
            {
                OrderItem orderItem = new OrderItem(tourId,tour.Name,new Price(tour.Price.Amount),shoppingCartDto.Id, false);
                _orderItemRepository.Create(orderItem);

                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartDto.Id);

                shoppingCart.AddItem((int)orderItem.Id);

                shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price,true);
                _shoppingCartRepository.Update(shoppingCart);
            }
            return Result.Ok(shoppingCartDto);

        }


        public Result<ShoppingCartDto> GetShoppingCartByUserId(int userId)
        {
            
            try
            {
                var shoppingCart = _shoppingCartRepository.GetShoppingCartByUserId(userId);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> RemoveItem(ShoppingCartDto shoppingCartDto, int itemId)
        {
             try
             {               
                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartDto.Id);
                OrderItem orderItem = GetOrderItemById(itemId);
                shoppingCart.RemoveItem(itemId);

                shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price, true);
                _shoppingCartRepository.Update(shoppingCart);
                _orderItemRepository.Delete(itemId);

                return Result.Ok(shoppingCartDto);
             }
             catch (ArgumentException e)
             {
                 return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
             }
        }

        private OrderItem GetOrderItemById(int id)
        {
            OrderItem orderItem = _orderItemRepository.Get(id);
            return orderItem;
        }
        
        
    }
}
