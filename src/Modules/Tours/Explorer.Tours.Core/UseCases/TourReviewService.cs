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
        public TourReviewService(ICrudRepository<TourReview> repository, IMapper mapper) : base(repository, mapper) { }

        public TourReviewService(ITourReviewRepository tourReviewRepository, ICrudRepository<TourReview> repository, IMapper mapper, ITourRepository tourRepository)
           : base(repository, mapper)
        {
            _tourReviewRepository = tourReviewRepository;
            _tourRepository = tourRepository;
        }

        /*
        public Result<TourReviewDto> Create(TourReviewDto tourReviewDto)
        {
            tourReviewDto.ReviewDate = DateTime.UtcNow;
            return base.Create(tourReviewDto);
        }*/
        
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

            tourReviewDto.ReviewDate = DateTime.UtcNow;
            var tour = _tourRepository.GetOne((int)tourReviewDto.TourId);
            TourReview tourReview = new TourReview(tourReviewDto.Grade, tourReviewDto.Comment, tourReviewDto.UserId, tourReviewDto.VisitDate, tourReviewDto.ReviewDate, tourReviewDto.Images, tour.Id);
            tour.AddTourReview(tourReview);
            //tour.TourReviews.Add(tourReview);
            _tourRepository.Update(tour);
            //tourReview = _tourReviewRepository.Create(tourReview);
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
