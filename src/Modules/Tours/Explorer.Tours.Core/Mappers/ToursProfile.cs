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
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        CreateMap<TourBundleDto, Tour>().ReverseMap();
        //CreateMap<PriceDto, Price>().ReverseMap();
        /*CreateMap<OrderItemDto, OrderItem>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        CreateMap<TourDto, Tour>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));*/
        /* CreateMap<OrderItemDto, OrderItem>()
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
             .AfterMap((src, dest) =>
             {
                 // Add debug statements to log the Price value
                 Debug.WriteLine($"Mapped Price: {src.Price.Amount} to {dest.Price.Amount}");
             });
         CreateMap<TourDto, Tour>().ReverseMap()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
            .AfterMap((src, dest) =>
            {
                // Add debug statements to log the Price value
                Debug.WriteLine($"Mapped Price: {src.Price.Amount} to {dest.Price}");
            });*/

        /*CreateMap<OrderItemDto, OrderItem>().IncludeAllDerived()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Select(h => new Price(h))));*/
        CreateMap<ObjectDto,Domain.Object>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<CheckpointVisitedDto, CheckpointVisited>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().ReverseMap();
        CreateMap<SecretDto, Secret>().ReverseMap();
        CreateMap<BundleDto, Bundle>().ReverseMap();
        CreateMap<TourSaleDto, TourSale>().ReverseMap();
    }
}