using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly ITouristSelectedEquipmentRepository _touristSelectedEquipmentRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ITourService _tourService;
    
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper, IEquipmentRepository equipmentRepository, ITourService tourService,ITouristSelectedEquipmentRepository touristEquipmentRepository) : base(repository, mapper) 
    {
        _touristSelectedEquipmentRepository = touristEquipmentRepository;
        _equipmentRepository = equipmentRepository;
        _tourService = tourService;
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


    public Result<PagedResult<EquipmentDto>> GetByIds(List<int> equipmentIds, int page, int pageSize)
    {

        try
        {
            var equipment = _equipmentRepository.GetEquipmentByIdsList(equipmentIds);
            var equipmentDtos = new List<EquipmentDto>();

            foreach (var eq in equipment)
            {
                var equipmentDto = MapToDto(eq);
                equipmentDtos.Add(equipmentDto);
            }


            var equipmentPagedResult = new PagedResult<EquipmentDto>(
                equipmentDtos, equipmentDtos.Count);
            if (equipmentPagedResult != null)
            {
                return Result.Ok(equipmentPagedResult);
            }
            else
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument);
        }
    }

    public Result<PagedResult<EquipmentDto>> GetByTourId(int tourId, int page, int pageSize)
    {
        try 
        {
           var pagedResult = GetPaged(page, pageSize);
           if (pagedResult != null)
           {
               TourDto tour = _tourService.Get(tourId).Value;
               return GetByIds(tour.Equipment, page, pageSize);
           }
           return Result.Fail("Equipment pagedResult is null");
        }
        catch (KeyNotFoundException e)
        {
           return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}