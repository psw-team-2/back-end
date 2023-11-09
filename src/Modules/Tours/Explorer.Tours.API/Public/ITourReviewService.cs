using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourReviewService
    {
        Result<PagedResult<TourReviewDto>> GetPaged(int page, int pageSize);
        Result<TourReviewDto> Create(TourReviewDto tourReviewDto, long loggedInUserId);
        //Result<TourReviewDto> Create(TourReviewDto tourReviewDt);
        Result<TourReviewDto> Update(TourReviewDto tourReview);
        Result Delete(int id);
        double GetAverageGradeForTour(int tourId);
        List<TourReviewDto> GetByTourId(int tourId);
    }
}
