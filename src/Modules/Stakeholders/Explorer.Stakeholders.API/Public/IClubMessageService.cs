using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubMessageService
    {
        Result<PagedResult<ClubMessageDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<ClubMessageDto>> GetByClubId(int clubId, int page, int pageSize);
        Result<ClubMessageDto> Create(ClubMessageDto clubMessage);


    }
}
