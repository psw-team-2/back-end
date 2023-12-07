using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Core.UseCases
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ICrudRepository<TourPurchaseToken> _tourPurchaseTokenRepository;
        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper) : base(repository, mapper)
        {
            _tourPurchaseTokenRepository = repository;
        }

    }
}
