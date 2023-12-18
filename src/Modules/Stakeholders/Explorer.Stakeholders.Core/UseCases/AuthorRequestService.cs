using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorRequestService : CrudService<AuthorRequestDto, AuthorRequest>, IAuthorRequestService
    {
        public AuthorRequestService(ICrudRepository<AuthorRequest> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
