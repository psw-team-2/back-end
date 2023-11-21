using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Mappers
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile() {
            CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();

        }
    }
}
