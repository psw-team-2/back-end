﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IBundleService
    {
        Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize);
        Result<BundleDto> Get(int id);
        Result<BundleDto> Create(BundleDto bundleDto);
        Result<BundleDto> Update(BundleDto bundleDto);
    }
}
