using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicRequestService : CrudService<PublicRequestDto, PublicRequest>, IPublicRequestService
    {
        public PublicRequestService(ICrudRepository<PublicRequest> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
