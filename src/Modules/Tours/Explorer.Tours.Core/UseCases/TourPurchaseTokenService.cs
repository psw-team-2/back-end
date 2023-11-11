using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Core.UseCases
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
