using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository
    {
        Club Create(Club club);
        Result<PagedResult<ClubDto>> GetAll();
        Club GetById(long id);
    }
}
