using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
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
        private readonly ITouristSelectedEquipmentRepository _touristSelectedEquipmentRepository;
        public TouristSelectedEquipmentService(ITouristSelectedEquipmentRepository touristEquipmentRepository, ICrudRepository<TouristSelectedEquipment> repository, IMapper mapper) : base(repository, mapper) 
        {
            _touristSelectedEquipmentRepository = touristEquipmentRepository;
        }

        public Result<TouristSelectedEquipmentDto> EquipmentSelection(TouristSelectedEquipmentDto dto)
        {
            TouristSelectedEquipment foundTouristEquipment = _touristSelectedEquipmentRepository.GetByTouristAndEquipment(dto.UserId, dto.EquipmentId);
            if (foundTouristEquipment == null)
            {
                return Create(dto);
            }
            return Delete((int)foundTouristEquipment.Id);
        }
     

    }
}

