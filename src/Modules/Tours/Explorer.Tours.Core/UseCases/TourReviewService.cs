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

        private readonly ITourRepository _tourRepository;

        private readonly ITourExecutionRepository _tourExecutionRepository;
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }

        public TourReviewService(ITourReviewRepository tourReviewRepository, ICrudRepository<TourReview> repository, IMapper mapper, ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository)
           : base(repository, mapper)
        {
            _tourReviewRepository = tourReviewRepository;
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
        }


        public Result<TourReviewDto> Create(TourReviewDto tourReviewDto, long loggedInUserId)
        {
            if (tourReviewDto.UserId != loggedInUserId)
            {
                return Result.Fail("User ID in the review does not match the logged-in user ID");
            }

            var purchaseToken = _tourReviewRepository.GetPurchaseToken((int)tourReviewDto.TourId, (int)loggedInUserId);

            if (purchaseToken == null)
            {
                return Result.Fail("No purchase token found for the specified tour and user");
            }
            var tourExecution = _tourExecutionRepository.GetTourExecutionForTourist((int)tourReviewDto.TourId, (int)loggedInUserId);

            if (tourExecution == null)
            {
                return Result.Fail("No tour execution found for the specified tour and user");
            }

            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

            if (tourExecution.LastActivity < oneWeekAgo)
            {
                return Result.Fail("Tour execution LastActivity is more than one week ago");
            }

            if (tourExecution.TourId != tourReviewDto.TourId || tourExecution.TouristId != loggedInUserId)
            {
                return Result.Fail("TourId or TouristId does not match the specified tour execution");
            }

            tourReviewDto.ReviewDate = DateTime.UtcNow;
            var tour = _tourRepository.GetOne((int)tourReviewDto.TourId);
            TourReview tourReview = new TourReview(tourReviewDto.Grade, tourReviewDto.Comment, tourReviewDto.UserId, tourReviewDto.VisitDate, tourReviewDto.ReviewDate, tourReviewDto.Images, tour.Id);
            tour.AddTourReview(tourReview);
            _tourRepository.Update(tour);
            return MapToDto(tourReview);
        }

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
