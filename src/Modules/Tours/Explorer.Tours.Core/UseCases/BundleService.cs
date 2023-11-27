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
            bundleDto.Price= 0;
            bundleDto.Status = BundleDto.BundleStatus.Draft;
            bundleDto.Tours = new List<int>();

            return base.Create(bundleDto);
        }

        public Result<BundleDto> Update(BundleDto bundleDto)
        {
            throw new NotImplementedException();
        }

    }
}
