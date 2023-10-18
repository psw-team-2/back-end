using AutoMapper;
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
        public ClubRequestService(ICrudRepository<ClubRequest> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<ClubRequestDto> SendRequest(ClubRequestDto clubRequest)
        {
            throw new NotImplementedException();
        }
    }
}
