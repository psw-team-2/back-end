﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.VisualBasic.CompilerServices;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserAccountAdministrationService : CrudService<UserAccountDto, User>, IUserAccountAdministrationService
    {
        public UserAccountAdministrationService(ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<UserAccountDto> GetUserAccountById(long userId)
        {
            return Get((int)userId);
        }
    }
}
