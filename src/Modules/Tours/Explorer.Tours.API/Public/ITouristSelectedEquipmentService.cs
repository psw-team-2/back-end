using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITouristSelectedEquipmentService
    {
        Result<PagedResult<TouristSelectedEquipmentDto>> GetPaged(int page, int pageSize);
        Result<TouristSelectedEquipmentDto> Create(TouristSelectedEquipmentDto touristSelectedEquipmentDto);
        Result<TouristSelectedEquipmentDto> Update(TouristSelectedEquipmentDto touristSelectedEquipmentDto);
        Result Delete(int id);

        Result<TouristSelectedEquipmentDto> EquipmentSelection(TouristSelectedEquipmentDto dto);
    }
}
