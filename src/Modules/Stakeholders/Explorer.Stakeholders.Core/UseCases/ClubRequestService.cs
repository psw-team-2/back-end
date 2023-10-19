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
            throw new NotImplementedException();
        }
       
    }
}
