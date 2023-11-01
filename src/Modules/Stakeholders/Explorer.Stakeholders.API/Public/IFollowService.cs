﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IFollowService
    {
        Result<FollowDto> Get(int id);
        Result<PagedResult<FollowDto>> GetPaged(int page, int pageSize);
        Result<FollowDto> Create(FollowDto follow);
        Result<FollowDto> Update(FollowDto follow);
    }
}
