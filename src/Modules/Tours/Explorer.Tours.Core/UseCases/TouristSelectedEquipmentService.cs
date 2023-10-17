using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristSelectedEquipmentService : CrudService<TouristSelectedEquipmentDto, TouristSelectedEquipment>, ITouristSelectedEquipmentService
    {
        public TouristSelectedEquipmentService(ICrudRepository<TouristSelectedEquipment> repository, IMapper mapper) : base(repository, mapper) { }
    
    
    
    
    }
}
