using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases
{
    public class TourService : CrudService<TourDto, Tour>, ITourService
    {
        public readonly ITourRepository _tourRepository;
        
        /*public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper) 
        {
            _tourRepository = tourRepository;
        }*/
        
        private readonly IUserAccountAdministrationService _userAccountService;

        public TourService(ICrudRepository<Tour> repository, IMapper mapper, IUserAccountAdministrationService userAccountService,ITourRepository tourRepository) : base(repository, mapper)
        {
            _userAccountService = userAccountService;
            _tourRepository = tourRepository;


        }

        public Result<List<TourDto>> GetToursListByAuthor(long authorId, int page, int pageSize)
        {


            var userResult = _userAccountService.Get((int)authorId);


            if (userResult.IsSuccess && userResult.Value != null)
            {
                var tours = base.GetPaged(page, pageSize);
                var authorsTours = tours.Value.Results.Where(tour => tour.AuthorId == authorId).ToList();

                return Result.Ok(authorsTours);

            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Failed to retrieve author information");
            }
        }

        public Result<TourDto> AddCheckPoint(TourDto tour, int checkPointId) {

            if (tour != null) 
            { 
                tour.CheckPoints.Add(checkPointId);
                Update(tour);
                return Result.Ok(tour);
            }
            return tour;
        }

        public Result<TourDto> DeleteCheckPoint(TourDto tour, int checkPointId)
        {
            if (tour != null)
            {
                tour.Equipment.Add(checkPointId);
                Update(tour);
                return Result.Ok(tour);
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);
            }
        }


        public Result<TourDto> AddEquipmentToTour(TourDto tour, int equipmentId)
        {
            if(tour != null)
            {
                tour.Equipment.Add(equipmentId);
                Update(tour);
                return Result.Ok(tour);
            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError(new KeyNotFoundException().Message);
            }
        }

        public Result<TourDto> RemoveEquipmentFromTour(TourDto tour, int equipmentId)
        {
            if (tour != null)
            {
                tour.Equipment.Remove(equipmentId);
                Update(tour);
            }
            return tour;
        }

        public Result<AverageGradeDto> GetAverageGradeForTour(int tourId)
        {
            var tour = _tourRepository.GetOne(tourId);
            if (tour == null)
            {
                return null;
            }
            double avg = tour.GetAverageGradeForTour();
            AverageGradeDto dto = new AverageGradeDto { AverageGrade = avg };
            return dto;
            //return avg;
        }
        public Result<TourExecutionDto> StartTour(int touristId, int tourId, double startLatitude, double startLongitude)
        {
            try
            {
                var tourExecutionDto = new TourExecutionDto
                {
                    TouristId = touristId,
                    TourId = tourId,
                    StartTime = DateTime.UtcNow,
                    CurrentLatitude = startLatitude,
                    CurrentLongitude = startLongitude
                };
                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }

        public Result<TourExecutionDto> CompleteTour(int tourExecutionId, double endLatitude, double endLongitude)
        {
            try
            {
                var tourExecutionDto = new TourExecutionDto
                {
                    Id = tourExecutionId,
                    EndTime = DateTime.UtcNow,
                };

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }

        public Result<TourExecutionDto> AbandonTour(int tourExecutionId, double abandonLatitude, double abandonLongitude)
        {
            try
            {
                var tourExecutionDto = new TourExecutionDto
                {
                    Id = tourExecutionId,
                    EndTime = DateTime.UtcNow,
                };

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourExecutionDto>(ex.Message);
            }
        }
        public List<TourReviewDto> GetByTourId(int tourId)
        {
            var reviews = _tourRepository.GetByTourId(tourId);

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
        public Result<TourDto> PublishTour(TourDto tour)
        {

                tour.Status = API.Dtos.AccountStatus.PUBLISHED;
                tour.PublishTime = DateTime.Now;

            return tour;
        }
        public Result<TourDto> ArchiveTour(TourDto tour)
        {
            tour.Status = API.Dtos.AccountStatus.ARCHIVED;
        
            return tour;
        }


    }
}
