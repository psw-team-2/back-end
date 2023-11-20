﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ITourPurchaseTokenService
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
        Result<TourPurchaseTokenDto> Create(TourPurchaseTokenDto tour);
        Result<TourPurchaseTokenDto> CreateTourPurchaseToken(List<OrderItemDto> orderItems, int userId);
    }
}
