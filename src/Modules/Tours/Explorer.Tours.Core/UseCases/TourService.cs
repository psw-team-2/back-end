using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Collections.Generic;

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
        public readonly IOrderItemService _orderItemService;
        public readonly IShoppingCartService _shoppingCartService;
        public readonly ITourExecutionService _tourExecutionService;

        public TourService(ICrudRepository<Tour> repository, IMapper mapper,
            IUserAccountAdministrationService userAccountService,
            IOrderItemService orderItemService, IShoppingCartService shoppingCartService,
            ITourExecutionService tourExecutionService
            , ITourRepository tourRepository) : base(repository, mapper)
        {
            _userAccountService = userAccountService;
            _tourRepository = tourRepository;
            _orderItemService = orderItemService;
            _shoppingCartService = shoppingCartService;
            _tourExecutionService = tourExecutionService;

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

        public Result<TourDto> AddCheckPoint(TourDto tour, int checkPointId)
        {

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
            if (tour != null)
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

        public Result<TourDto> AddObjectToTour(TourDto tour, int tourObjectId)
        {

            if (tour != null)
            {
                tour.Objects.Add(tourObjectId);
                Update(tour);
                return Result.Ok(tour);
            }

            return tour;
        }

        public Result<TourDto> RemoveObjectFromTour(TourDto tour, int tourObjectId)
        {
            if (tour != null)
            {
                tour.Objects.Remove(tourObjectId);
                Update(tour);
            }

            return tour;
        }


        public Result<AverageGradeDto> GetAverageGradeForTour(int tourId)
        {
            try
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
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<AverageGradeDto> GetAverageWeeklyGradeForTour(int tourId)
        {
            try
            {
                var tour = _tourRepository.GetOne(tourId);
                if (tour == null)
                {
                    return null;
                }

                double avg = tour.GetWeeklyAverageGradeForTour();
                AverageGradeDto dto = new AverageGradeDto { AverageGrade = avg };
                return dto;
                //return avg;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> StartTour(int touristId, int tourId, double startLatitude,
            double startLongitude)
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

        public Result<TourExecutionDto> AbandonTour(int tourExecutionId, double abandonLatitude,
            double abandonLongitude)
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

        public Result<PagedResult<TourDto>> RetrivesAllUserTours(int userId, int page, int pageSize)
        {
            var userResult = _userAccountService.Get((int)userId);
            if (userResult.IsSuccess && userResult.Value != null)
            {
                var shoppingCart = _shoppingCartService.GetShoppingCartByUserId(userId);
                var shoppingCartItems = _orderItemService.GetBoughtShoppingItemsFromCart(shoppingCart.Value.Id);
                //var tourIds = shoppingCartItems.Value.Select(item => item.TourId).ToList();
                var tours = base.GetPaged(page, pageSize);
                var userTours = tours.Value.Results
                    .Where(tour => shoppingCartItems.Value.Any(item => item.ItemId == tour.Id)).ToList();

                var result = new PagedResult<TourDto>(userTours, userTours.Count);

                return Result.Ok(result);

            }
            else
            {
                return Result.Fail(FailureCode.NotFound).WithError("Failed to retrieve author information");
            }
        }

        public List<TourBundleDto> GetToursByAuthorId(int authorId)
        {
            List<Tour> tours = _tourRepository.GetToursByAuthorId(authorId);
            // List<TourDto> dto = MapToDto(tours);

            var toursDto = tours.Select(tour => new TourBundleDto
            {
                Id = (int)tour.Id,
                FootTime = tour.FootTime,
                BicycleTime = tour.BicycleTime,
                CarTime = tour.CarTime,
                TotalLength = tour.TotalLength,
                AuthorId = tour.AuthorId,
                PublishTime = tour.PublishTime,
                Name = tour.Name,
                Description = tour.Description,
                Status = (API.Dtos.AccountStatus)tour.Status,
                Difficulty = tour.Difficulty,
                Price = tour.Price,
                Image = tour.Image
            }).ToList();

            return toursDto;
        }

        public Result<List<TourDto>> GetToursFromSaleById(List<long> tourIds)
        {
            var foundTours = new List<Tour>();
            foreach (var id in tourIds)
            {
                var tour = _tourRepository.Get(id);

                foundTours.Add(tour);
            }

            return MapToDto(foundTours);
        }

        public Result<PagedResult<TourDto>> GetActiveTours(List<long> tourIds)
        {
            try
            {
                var tourExecutions = _tourExecutionService.GetActiveExecutedToursByTour(tourIds);
                List<long> activeTourIds = new List<long>();

//                    throw new Exception($"Tour Executions: {string.Join(",", tourExecutions)}");

                foreach (var execution in tourExecutions)
                {
                    activeTourIds.Add(execution.TourId);
                }

                //                   throw new Exception($"Active Tour Ids: {string.Join(",", activeTourIds)}");
                var tours = _tourRepository.GetByIds(activeTourIds);
                //                       throw new Exception($"Tours: {string.Join(",", tours)}");


                var tourDtos = MapToDto(tours).Value;

                var toursPagedResult = new PagedResult<TourDto>(
                    tourDtos,
                    tourDtos.Count
                );

                return Result.Ok(toursPagedResult).WithSuccess("Tour obtained");
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }


    }
}
