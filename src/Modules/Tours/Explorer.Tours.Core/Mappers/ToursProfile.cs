using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourProblemDto, TourProblem>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TourInfoDto, TourInfo>().ReverseMap();
        CreateMap<CheckPointDto, CheckPoint>().ReverseMap();
        CreateMap<TouristSelectedEquipmentDto, TouristSelectedEquipment>().ReverseMap();
        CreateMap<PublicRequestDto, PublicRequest>().ReverseMap();   
        CreateMap<TourProblemResponseDto, TourProblemResponse>().ReverseMap();
        CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        //CreateMap<PriceDto, Price>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        

    }
}