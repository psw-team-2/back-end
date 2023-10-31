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
    public interface IApplicationReviewService
    {
        Result<PagedResult<ApplicationReviewDto>> GetPaged(int page, int pageSize);

        Result<ApplicationReviewDto> Create(ApplicationReviewDto applicationReviewDto);
        Result<ApplicationReviewDto> Update(ApplicationReviewDto applicationReviewDto);
        Result Delete(int id);
    }
}
