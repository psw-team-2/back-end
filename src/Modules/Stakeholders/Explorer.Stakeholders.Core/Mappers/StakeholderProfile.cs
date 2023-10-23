using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<ProfileDto, Explorer.Stakeholders.Core.Domain.Profile>().ReverseMap();
    }
}