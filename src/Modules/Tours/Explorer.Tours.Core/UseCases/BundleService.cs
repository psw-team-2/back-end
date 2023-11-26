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
    public class BundleService : CrudService<BundleDto, Bundle>, IBundleService
    {
        public BundleService(ICrudRepository<Bundle> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<BundleDto> Create(BundleDto bundleDto)
        {
            throw new NotImplementedException();
        }

        public Result<BundleDto> AddTour(BundleDto bundleDto)
        {
            throw new NotImplementedException();
        }

    }
}
