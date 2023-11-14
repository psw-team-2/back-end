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
using Explorer.Tours.API.Public;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class OrderItemService : CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(ICrudRepository<OrderItem> repository, IMapper mapper, IOrderItemRepository orderItemRepository) : base(repository, mapper)
        {
            _orderItemRepository = orderItemRepository;
        }

        public Result<IEnumerable<OrderItemDto>> GetOrderItemsByShoppingCart(int shoppingCartId)
        {
            IEnumerable<OrderItem> orderItems = _orderItemRepository.GetOrderItemsByShoppingCart(shoppingCartId);
            List<OrderItemDto> dtosForItems = new List<OrderItemDto>();
            foreach (var item in orderItems)
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
            
            return dtosForItems;
        }


    }

}
