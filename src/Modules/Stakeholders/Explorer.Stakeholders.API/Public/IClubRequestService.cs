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
    public interface IClubRequestService
    {
        Result<PagedResult<ClubRequestDto>> GetPaged(int page, int pageSize);
        Result<ClubRequestDto> Create(ClubRequestDto clubRequest);
        Result<ClubRequestDto> Update(ClubRequestDto clubRequest);
        Result Delete(int id);
        Result<ClubRequestDto> SendRequest(ClubRequestDto clubRequests);
    }
}
