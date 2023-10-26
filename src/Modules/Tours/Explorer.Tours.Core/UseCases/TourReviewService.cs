using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TourReviewDto> Create(TourReviewDto tourReviewDto)
        {
            tourReviewDto.ReviewDate = DateTime.UtcNow;
            return base.Create(tourReviewDto);
        }
    }
}
