﻿using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TouristSelectedEquipmentDto, TouristSelectedEquipment>().ReverseMap();
        CreateMap<ObjectDto,Domain.Object>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();    
    }
}