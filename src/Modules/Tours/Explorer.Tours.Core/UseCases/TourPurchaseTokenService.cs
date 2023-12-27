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
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ICrudRepository<TourPurchaseToken> _tourPurchaseTokenRepository;
        private readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepositoryAdditional;

        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper, ITourPurchaseTokenRepository tourPurchaseTokenRepository) : base(repository, mapper)
        {
            _tourPurchaseTokenRepository = repository;
            _tourPurchaseTokenRepositoryAdditional = tourPurchaseTokenRepository;
        }

        public Result<PagedResult<TourPurchaseTokenDto>> GetTourPurchaseTokensByTourId(int tourId)
        {
            try
            {
                var tourPurchaseTokens = _tourPurchaseTokenRepositoryAdditional.GetByTour(tourId);

                var tourPurchaseTokensDtos = MapToDto(tourPurchaseTokens).Value;

                var tourExecutionPagedResult = new PagedResult<TourPurchaseTokenDto>(
                    tourPurchaseTokensDtos,
                    tourPurchaseTokensDtos.Count
                );

                return Result.Ok(tourExecutionPagedResult).WithSuccess("Tour Purchase Tokens obtained");
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

        public Result<PagedResult<TourPurchaseTokenDto>> GetWeeklyTourPurchaseTokensByTourId(int tourId)
        {
            try
            {
                var tourPurchaseTokens = _tourPurchaseTokenRepositoryAdditional.GetWeeklyByTour(tourId);

                var tourPurchaseTokensDtos = MapToDto(tourPurchaseTokens).Value;

                var tourExecutionPagedResult = new PagedResult<TourPurchaseTokenDto>(
                    tourPurchaseTokensDtos,
                    tourPurchaseTokensDtos.Count
                );

                return Result.Ok(tourExecutionPagedResult).WithSuccess("Tour Purchase Tokens obtained");
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }

    }
}
