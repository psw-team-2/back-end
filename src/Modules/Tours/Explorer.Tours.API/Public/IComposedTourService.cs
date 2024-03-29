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
    public interface IComposedTourService
    {
        Result<ComposedTourDto> Create(ComposedTourDto composedTourDto);
        Result<PagedResult<ComposedTourDto>> GetPaged(int pageNumber, int pageSize);
    }
}
