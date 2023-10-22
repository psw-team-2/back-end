using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProfileService
    {
        Result<ProfileDto> Get(int id);
        Result<ProfileDto> GetByUserId(int id);
        Result<ProfileDto> Create(ProfileDto profiles);
        Result<ProfileDto> Update(ProfileDto profiles);
    }
}
