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

        public Result<OrderItemDto> GetAllByShoppingCartId(int shoppingCartId)
        {
            OrderItem orderItems =  _orderItemRepository.GetAllByShoppingCartId(shoppingCartId);
            OrderItemDto orderItemsDto = MapToDto(orderItems);
            return Result.Ok(orderItemsDto);
        }
    }

}
