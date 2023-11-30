using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
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
    public class BundleService : CrudService<BundleDto, Bundle>, IBundleService
    {
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly IBundleRepository _bundleRepository;
        public BundleService(ICrudRepository<Bundle> crudRepository, IMapper mapper, ICrudRepository<Tour> tourRepository, IBundleRepository bundleRepository) : base(crudRepository, mapper)
        {
            _tourRepository = tourRepository;
            _bundleRepository = bundleRepository;
        }

        public override Result<BundleDto> Create(BundleDto bundleDto)
        {
            bundleDto.Price = 0;
            bundleDto.Status = BundleStatus.Draft;
            bundleDto.Tours = new List<int>();

            return base.Create(bundleDto);
        }



        public Result<BundleDto> PublishBundle(int bundleId)
        {
            var existingBundle = _bundleRepository.GetById(bundleId);

            if (existingBundle != null)
            {

                int publishedTourCount = 0;
                foreach (int tourId in existingBundle.Tours)
                {
                    var tour = _tourRepository.Get(tourId);
                    if (tour.Status == Domain.AccountStatus.PUBLISHED)
                    {
                        publishedTourCount++;
                    }
                }

                if (publishedTourCount >= 2)
                {
                    existingBundle.Status = Bundle.BundleStatus.Published;
                }
                else
                {
                    existingBundle.Status = Bundle.BundleStatus.Draft;
                }


                _bundleRepository.Update(existingBundle);

                return MapToDto(existingBundle);
            }
            return null;

        }

        public List<BundleDto> GetBundlesByAuthorId(int authorId)
        {
            var reviews = _bundleRepository.GetBundlesByAuthorId(authorId);

            // Perform the necessary mapping to DTOs here.
            var reviewsDto = reviews.Select(review => new BundleDto
            {
                Name = review.Name,
                Price = review.Price,
                UserId = (int)review.UserId,
                Id = (int)review.Id,
                Status = (API.Dtos.BundleStatus)review.Status
            }).ToList();

            return reviewsDto;
        }

        public Result<BundleDto> AddTour(BundleDto bundleDto, int tourId)
        {
            try
            {
                Tour tour = _tourRepository.Get(tourId);
                if (bundleDto != null)
                {
                    Bundle bundle = _bundleRepository.GetById(bundleDto.Id);
                    bundle.AddTour((int)tour.Id);

                    //bundle.CalculateTotalPrice(bundle.Price, tour.Price, true);
                    bundle.Price += tour.Price;
                    _bundleRepository.Update(bundle);
                    return Result.Ok(bundleDto);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BundleDto> RemoveTour(int bundleId, int tourId)
        {
            throw new NotImplementedException();
        }
    }
}
