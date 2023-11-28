using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
        private IBundleRepository _bundleRepository;
        public BundleService(ICrudRepository<Bundle> crudRepository, IMapper mapper, IBundleRepository bundleRepository) : base(crudRepository, mapper)
        {
            _bundleRepository = bundleRepository;
        }

        public Result<BundleDto> Create(BundleDto bundleDto)
        {
            bundleDto.Price= 0;
            bundleDto.Status = BundleDto.BundleStatus.Draft;
            bundleDto.Tours = new List<TourDto>();

            return base.Create(bundleDto);
        }

     

        public Result<BundleDto> PublishBundle(int bundleId)
        {
            var existingBundle = _bundleRepository.GetById(bundleId);

            if (existingBundle != null)
            {

                int publishedTourCount = 0;
                foreach (var tour in existingBundle.Tours)
                {
                    if (tour.Status.Equals(1))
                    {
                        publishedTourCount++;
                    }
                }

                if (publishedTourCount >= 2)
                {
                    existingBundle.Status.Equals(1);
                }
                else
                {
                    existingBundle.Status.Equals(0);
                }


                _bundleRepository.Update(existingBundle);

                return MapToDto(existingBundle);
            }
            return null;
 
        }

    }
}
