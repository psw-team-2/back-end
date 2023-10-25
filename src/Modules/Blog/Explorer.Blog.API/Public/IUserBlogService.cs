﻿using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IUserBlogService
    {
        Result<PagedResult<UserBlogDto>> GetPaged(int page, int pageSize);

        Result<UserBlogDto> Create(UserBlogDto blog);
        Result<UserBlogDto> Update(UserBlogDto blog);
        Result Delete(int id);

        Result<UserBlogDto> Get(int id);
    }
}