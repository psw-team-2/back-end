namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository
    {
        Club Create(Club club);
        List<Club> GetAll();
        Club GetById(long id);
    }
}
