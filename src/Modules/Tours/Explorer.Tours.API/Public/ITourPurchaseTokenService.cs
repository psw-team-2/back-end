using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourPurchaseTokenService
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
        public Result<PagedResult<TourPurchaseTokenDto>> GetTourPurchaseTokensByTourId(int tourId);
        public Result<PagedResult<TourPurchaseTokenDto>> GetWeeklyTourPurchaseTokensByTourId(int tourId);
    }
}
