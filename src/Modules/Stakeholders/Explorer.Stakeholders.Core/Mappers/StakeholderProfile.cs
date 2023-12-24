using AutoMapper;
using ProfileD = Explorer.Stakeholders.Core.Domain.Users.Profile;
using ProfileA = AutoMapper.Profile;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : ProfileA
{
    public StakeholderProfile()
    {
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<ClubRequestDto, ClubRequest>().ReverseMap();
        CreateMap<ClubMessageDto, ClubMessage>().ReverseMap();
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<ProfileDto, ProfileD>().ReverseMap();
        CreateMap<UserAccountDto, User>().ReverseMap();
        CreateMap<ApplicationReviewDto, ApplicationReview>().ReverseMap();
        CreateMap<FollowDto, Follow>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<AnswerDto, Answer>().ReverseMap();
    }
}