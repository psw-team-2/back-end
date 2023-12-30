using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAuthorRequestService
    {
        Result<AuthorRequestDto> Get(int id);
        Result<PagedResult<AuthorRequestDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<AuthorRequestDto>> GetAllUnderReview(int page, int pageSize);
        Result<AuthorRequestDto> Create(AuthorRequestDto authorRequest);
        Result<AuthorRequestDto> Update(AuthorRequestDto authorRequest);
    }
}
