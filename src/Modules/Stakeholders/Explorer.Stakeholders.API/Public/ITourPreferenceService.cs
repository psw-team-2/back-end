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
    public interface ITourPreferenceService
    {
        Result<PagedResult<TourPreferenceDto>> GetPaged(int page, int pageSize);
        Result<TourPreferenceDto> Create(TourPreferenceDto equipment);
        Result<TourPreferenceDto> Update(TourPreferenceDto equipment);
        Result Delete(int id);
        Result<TourPreferenceDto> GetByTouristId(int touristId);
    }
}
