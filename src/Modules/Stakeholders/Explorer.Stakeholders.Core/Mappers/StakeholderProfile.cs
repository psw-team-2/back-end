using AutoMapper;
using ProfileD = Explorer.Stakeholders.Core.Domain.Profile;
using ProfileA = AutoMapper.Profile;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : ProfileA
{
    public StakeholderProfile()
    {
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<ProfileDto, ProfileD>().ReverseMap();
        CreateMap<UserAccountDto, User>().ReverseMap();
    }
}