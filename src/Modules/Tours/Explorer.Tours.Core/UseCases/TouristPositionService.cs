using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristPositionService : CrudService<TouristPositionDto, TouristPosition>, ITouristPositionService
    {
        public TouristPositionService(ICrudRepository<TouristPosition> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<TouristPositionDto> GetByUser(long userId)
        {
            Result<PagedResult<TouristPositionDto>> pagedResult = GetPaged(1, int.MaxValue);
            TouristPositionDto foundItem;
            if (pagedResult.IsSuccess)
            {
                PagedResult<TouristPositionDto> pagedData = pagedResult.Value;
                List<TouristPositionDto> allItems = pagedData.Results;
                foundItem = allItems.FirstOrDefault(dto => dto.UserId == userId);

            }
            else
            {
                return new Result<TouristPositionDto>();
            }

            return foundItem;
        }
    }
}
