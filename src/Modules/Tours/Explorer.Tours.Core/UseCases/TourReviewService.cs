using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
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
        private readonly ITourReviewRepository _tourReviewRepository;
  

        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }

        public TourReviewService(ITourReviewRepository tourReviewRepository, ICrudRepository<TourReview> repository, IMapper mapper)
           : base(repository, mapper)
        {
            _tourReviewRepository = tourReviewRepository;
        }
        public Result<TourReviewDto> Create(TourReviewDto tourReviewDto)
        {
            tourReviewDto.ReviewDate = DateTime.UtcNow;
            return base.Create(tourReviewDto);
        }
        /*
        public Result<TourReviewDto> Create(TourReviewDto tourReviewDto, long loggedInUserId)
        {
            // Check if the logged in user ID matches the purchaseToken user ID
            if (tourReviewDto.UserId != loggedInUserId)
            {
                return Result.Fail("User ID in the review does not match the logged-in user ID");
            }

            // Check if there is a purchase token with matching tourId and UserId
            var purchaseToken = _tourReviewRepository.GetPurchaseToken((int)tourReviewDto.TourId, (int)loggedInUserId);

            if (purchaseToken == null)
            {
                return Result.Fail("No purchase token found for the specified tour and user");
            }

            // Now you can proceed with creating the review since the checks passed
            tourReviewDto.ReviewDate = DateTime.UtcNow;
            return base.Create(tourReviewDto);
        }*/

        public double GetAverageGradeForTour(int tourId)
        {
            var tourReviews = _tourReviewRepository.GetReviewsForTour(tourId);

            if (tourReviews != null && tourReviews.Any())
            {
                double averageGrade = tourReviews.Average(review => review.Grade);
                return averageGrade;
            }
            else
            {
               
                return 0.0; 
            }
        }
        public List<TourReviewDto> GetByTourId(int tourId)
        {
            var reviews = _tourReviewRepository.GetByTourId(tourId);

            // Perform the necessary mapping to DTOs here.
            var reviewsDto = reviews.Select(review => new TourReviewDto
            {
                Grade = review.Grade,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                VisitDate = review.VisitDate,
                UserId = review.UserId,
                TourId = review.TourId,
                Images = review.Images,
                Id = (int)review.Id
            }).ToList();

            return reviewsDto;
        }






    }
}
