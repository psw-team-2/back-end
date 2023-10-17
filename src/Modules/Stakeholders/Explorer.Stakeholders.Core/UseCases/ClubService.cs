using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        /*private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public Result<PagedResult<TDto>> GetPaged(int page, int pageSize)
        {
            var result = CrudRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }*/

        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
