using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;
using ProfileD = Explorer.Stakeholders.Core.Domain.Profile;
using ProfileA = AutoMapper.Profile;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : ProfileA
{
    public StakeholderProfile()
    {
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<ProfileDto, ProfileD>().ReverseMap();
    }
}