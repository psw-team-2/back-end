using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubRequestService : CrudService<ClubRequestDto, ClubRequest>, IClubRequestService
    {
        protected readonly ICrudRepository<ClubRequest> CrudRepository;
        private readonly IClubRequestService _clubRequestService;
        private readonly IClubService _clubService;

        public ClubRequestService(ICrudRepository<ClubRequest> repository, IMapper mapper) : base(repository, mapper)
        {
            CrudRepository = repository;
        }
        
        public Result<ClubRequestDto> SendRequest(ClubRequestDto clubRequests)
        {
            try
            {
                var result = CrudRepository.Create(MapToDomain(clubRequests));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ClubRequestDto> WithdrawRequest(int id)
        {
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok<ClubRequestDto>(null);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ClubRequestDto> GetClubRequestById(int id)
        {
            try
            {
                var request = CrudRepository.Get(id);

                if (request == null)
                {
                    return Result.Fail<ClubRequestDto>(FailureCode.NotFound).WithError("Request not found");
                }

                var requestDto = MapToDto(request);
                return Result.Ok(requestDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ClubRequestDto> AcceptRequest(ClubRequestDto clubRequest)
        {
            try
            {
                var existingRequestResult = GetExistingRequest(clubRequest.Id);
                var clubResult = GetClub(clubRequest.ClubId);

                if (existingRequestResult.IsSuccess && clubResult.IsSuccess)
                {
                    var existingRequest = existingRequestResult.Value;
                    var club = clubResult.Value;

                    UpdateClubRequest(existingRequest);
                    UpdateClub(club, clubRequest.AccountId);

                    return Result.Ok<ClubRequestDto>(clubRequest);
                }
                else
                {
                    return Result.Fail(FailureCode.Internal).WithError("Error occurred.");
                }
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private Result<ClubRequestDto> GetExistingRequest(int requestId)
        {
            var existingRequestResult = _clubRequestService.GetClubRequestById(requestId);
            if (existingRequestResult.IsSuccess)
            {
                return existingRequestResult;
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Request not found");
            }
        }

        private Result<ClubDto> GetClub(int clubId)
        {
            var clubResult = _clubService.GetClubById(clubId);
            if (clubResult.IsSuccess)
            {
                return clubResult;
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Club not found");
            }
        }

        private void UpdateClubRequest(ClubRequestDto existingRequest)
        {
            existingRequest.RequestStatus = (API.Dtos.RequestStatusEnum)Domain.RequestStatusEnum.Accepted;
            _clubRequestService.Update(existingRequest);
        }

        private void UpdateClub(ClubDto club, long accountId)
        {
            club.MemberIds.Add(accountId);
            _clubService.Update(club);
        }

        public Result<ClubRequestDto> RejectRequest(ClubRequestDto clubRequest)
        {
            try
            {
                var existingRequestResult = GetExistingRequest(clubRequest.Id);

                if (existingRequestResult.IsSuccess)
                {
                    var existingRequest = existingRequestResult.Value;

                    UpdateRequest(existingRequest);

                    return Result.Ok<ClubRequestDto>(clubRequest);
                }
                else
                {
                    return Result.Fail(FailureCode.Internal).WithError("Error occurred.");
                }
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private void UpdateRequest(ClubRequestDto request)
        {
            request.RequestStatus = (API.Dtos.RequestStatusEnum)Domain.RequestStatusEnum.Rejected;
            _clubRequestService.Update(request);
        }
    }
}
