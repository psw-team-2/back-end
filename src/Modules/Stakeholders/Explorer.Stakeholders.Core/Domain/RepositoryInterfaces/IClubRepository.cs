namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository
    {
        Club Create(Club club);
        //Result<PagedResult<ClubDto>> GetAll();
        Club GetById(long id);
    }
}
