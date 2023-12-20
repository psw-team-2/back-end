using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AuthorRequestService : CrudService<AuthorRequestDto, AuthorRequest>, IAuthorRequestService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRequestRepository _authorRequestRepository;
        public AuthorRequestService(ICrudRepository<AuthorRequest> repository, IMapper mapper, IAuthorRequestRepository authorRequestRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _authorRequestRepository = authorRequestRepository;
        }

        public Result<PagedResult<AuthorRequestDto>> GetAllUnderReview(int page, int pageSize)
        {
            var underReviewRequests = _authorRequestRepository.GetAll()
                .Where(request => request.RequestStatus == 0)
                .ToList();

            var underReviewRequestsDtos = _mapper.Map<List<AuthorRequestDto>>(underReviewRequests);

            var pagedResult = Result.Ok(new PagedResult<AuthorRequestDto>(underReviewRequestsDtos, underReviewRequestsDtos.Count));

            return pagedResult;

        }
    }
}
