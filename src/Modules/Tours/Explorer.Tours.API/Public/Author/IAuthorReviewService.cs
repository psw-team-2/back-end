using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Author
{
    public interface IAuthorReviewService
    {
        Result<PagedResult<AuthorReviewDto>> GetPaged(int page, int pageSize);
        Result<AuthorReviewDto> Create(AuthorReviewDto authorReviewDto, int touristId);
        Result<PagedResult<AuthorReviewDto>> GetAuthorReviews(int authorId);
        Result<AuthorReviewDto> DisapproveAuthorReview(long reviewId);
    }
}
