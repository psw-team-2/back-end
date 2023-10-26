using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly ITouristSelectedEquipmentRepository _touristSelectedEquipmentRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper, IEquipmentRepository equipmentRepository, ITouristSelectedEquipmentRepository touristEquipmentRepository) : base(repository, mapper) 
    {
        _touristSelectedEquipmentRepository = touristEquipmentRepository;
        _equipmentRepository = equipmentRepository;
    }
    public Result<IEnumerable<EquipmentForSelectionDto>> GetAllForSelection(int userId)
    {
        IEnumerable<Equipment> equipment = _equipmentRepository.GetAll();
        List<EquipmentForSelectionDto> dtosForSelection = new List<EquipmentForSelectionDto>();
        foreach (var equipmentItem in equipment)
        {
            bool isSelected = _touristSelectedEquipmentRepository.GetByTouristAndEquipment(userId, equipmentItem.Id) != null;
            EquipmentForSelectionDto dto = new EquipmentForSelectionDto(equipmentItem.Id, equipmentItem.Name, equipmentItem.Description, isSelected);
            dtosForSelection.Add(dto);
        }

        return dtosForSelection;
    }
}