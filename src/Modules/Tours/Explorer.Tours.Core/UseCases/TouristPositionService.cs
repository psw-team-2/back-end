using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases
{
    public class TouristPositionService : CrudService<TouristPositionDto, TouristPosition>, ITouristPositionService
    {
        public TouristPositionService(ICrudRepository<TouristPosition> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
