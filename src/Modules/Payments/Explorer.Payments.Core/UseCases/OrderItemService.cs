using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class OrderItemService : CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(ICrudRepository<OrderItem> repository, IMapper mapper, IOrderItemRepository orderItemRepository) : base(repository, mapper)
        {
            _orderItemRepository = orderItemRepository;
        }

        public Result<IEnumerable<OrderItemDto>> GetBoughtShoppingItemsFromCart(int shoppingCartId)
        {
            IEnumerable<OrderItem> orderItems = _orderItemRepository.GetOrderItemsByShoppingCart(shoppingCartId);
            List<OrderItemDto> dtosForItems = new List<OrderItemDto>();
            foreach (var item in orderItems)
            {
                if (item.IsBought == true)
                {
                    OrderItemDto dto = new OrderItemDto
                    {
                        Id = (int)item.Id,
                        TourId = item.TourId,
                        TourName = item.TourName,
                        Price = item.Price,
                        ShoppingCartId = shoppingCartId,
                        IsBought = item.IsBought

                    };
                    dtosForItems.Add(dto);
                }

            }

            return dtosForItems;
        }

        public Result<IEnumerable<OrderItemDto>> GetOrderItemsByShoppingCart(int shoppingCartId)
        {
            IEnumerable<OrderItem> orderItems = _orderItemRepository.GetOrderItemsByShoppingCart(shoppingCartId);
            List<OrderItemDto> dtosForItems = new List<OrderItemDto>();
            foreach (var item in orderItems)
            {
                if(item.IsBought == false)
                {
                    OrderItemDto dto = new OrderItemDto
                    {
                        Id = (int)item.Id,
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        Price = item.Price,
                        ShoppingCartId = shoppingCartId,
                        IsBought = item.IsBought,
                        IsBundle = item.IsBundle
                    };
                    dtosForItems.Add(dto);
                }
                
            }
            
            return dtosForItems;
        }


    }

}
