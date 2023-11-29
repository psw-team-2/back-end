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
        public BundleService(IBundleRepository bundleRepository, ICrudRepository<Bundle> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _bundleRepository = bundleRepository;
        }
        public IBundleRepository _bundleRepository;

        public override Result<BundleDto> Create(BundleDto bundleDto)
        {
            bundleDto.Price= 0;
            bundleDto.Status = API.Dtos.BundleStatus.Draft;
            bundleDto.Tours = new List<int>();

            return base.Create(bundleDto);
        }

        public Result<BundleDto> Update(BundleDto bundleDto)
        {
            throw new NotImplementedException();
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

    }
}
