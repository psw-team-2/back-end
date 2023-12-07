using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Public;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly ITourService _tourService;
        private readonly IMessageService _messageService;
        public ClubService(ICrudRepository<Club> repository, IMapper mapper, ITourService tourService, IMessageService messageService) : base(repository, mapper) 
        {
            _tourService = tourService;
            _messageService = messageService;
        }

        public Result<ClubDto> Kick(ClubDto club)
        {
            throw new NotImplementedException();
        }

        public Result<ClubDto> GetClubById(int id)
        {
            try
            {
                var club = CrudRepository.Get(id);

                if (club == null)
                {
                    return Result.Fail<ClubDto>(FailureCode.NotFound).WithError("Club not found");
                }

                var clubDto = MapToDto(club);
                return Result.Ok(clubDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<long>> GetAllMembers(int clubId)
        {
            try
            {
                var club = CrudRepository.Get(clubId);
                if(club == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Club not found");
                }

                var members = club.MemberIds;
                return Result.Ok(members);
            }
            catch(ArgumentException e) 
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<bool> InviteMembersToTour(long clubId, int senderId, int tourId, List<long> invitedMemberIds)
        
         {
            try
            {
                var club = CrudRepository.Get(clubId);
                if (club == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Club not found.");
                }

                var tourResult = _tourService.Get(tourId);
                if (tourResult.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
                }
                var tour = tourResult.Value;


                if (!club.MemberIds.Contains(senderId))
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Sender is not a member of the club.");
                }

                if (tour.AuthorId != senderId)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Sender didn't start this tour.");
                }

                foreach (var memberId in invitedMemberIds)
                {
                    var messageDto = new MessageDto
                    {
                        SenderId = senderId,
                        ReceiverId = memberId,
                        MessageContent = $"You are invited to join the tour {tour.Name}.",
                        Status = 0
                    };

                    var notification = _messageService.Create(messageDto);
                }

                return Result.Ok(true);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }

    }
}
