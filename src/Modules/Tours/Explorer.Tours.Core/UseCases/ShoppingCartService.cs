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
        private readonly ICrudRepository<OrderItem> _crudOrderItemRepository;
        private readonly IOrderItemRepository _orderItemRepository;


        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository shoppingCartRepository, ICrudRepository<Tour> tourRepository, ICrudRepository<OrderItem> crudOrderItemRepository, IOrderItemRepository orderItemRepository) : base(repository, mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _tourRepository = tourRepository;
            _crudOrderItemRepository = crudOrderItemRepository;
            _orderItemRepository = orderItemRepository;
        }


        public Result<ShoppingCartDto> AddItem(ShoppingCartDto shoppingCartDto, int tourId)
        {
            try
            {
                Tour tour = _tourRepository.Get(tourId);
                if (shoppingCartDto != null)
                {
                    OrderItem orderItem = new OrderItem(tourId, tour.Name, tour.Price, shoppingCartDto.Id, false);
                    _crudOrderItemRepository.Create(orderItem);

                    ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartDto.Id);

                    shoppingCart.AddItem((int)orderItem.Id);

                    shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price, true);
                    _shoppingCartRepository.Update(shoppingCart);
                    return Result.Ok(shoppingCartDto);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }           

        }


        public Result<ShoppingCartDto> GetShoppingCartByUserId(int userId)
        {

            try
            {
                var shoppingCart = _shoppingCartRepository.GetShoppingCartByUserId(userId);
                ShoppingCartDto shoppingCartDto = MapToDto(shoppingCart);
                return Result.Ok(shoppingCartDto);
            }
            catch (Exception e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId)
        {
            try
            {
                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartId);
                OrderItem orderItem = GetOrderItemById(itemId);
                shoppingCart.RemoveItem(itemId);

                shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price, false);
                _shoppingCartRepository.Update(shoppingCart);
                _crudOrderItemRepository.Delete(itemId);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Result<ShoppingCartDto> RemoveAllItems(int shoppingCartId)
        {
            try
            {
                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartId);
                shoppingCart.RemoveAllItems();
                _orderItemRepository.RemoveAllItemsByShoppingCartId(shoppingCartId);
                _shoppingCartRepository.Update(shoppingCart);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private OrderItem GetOrderItemById(int id)
        {
            OrderItem orderItem = _crudOrderItemRepository.Get(id);
            return orderItem;
        }

        public Result<double> GetTotalPriceByUserId(int userId)
        {
            try
            {
                double TotalPrice = _shoppingCartRepository.GetTotalPriceByUserId(userId);
                return Result.Ok(TotalPrice);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<double>(FailureCode.InvalidArgument).WithError(e.Message);
            }

        }
    }
}
