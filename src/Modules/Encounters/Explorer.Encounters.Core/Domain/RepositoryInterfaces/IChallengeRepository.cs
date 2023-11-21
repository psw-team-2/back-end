namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IChallengeRepository
    {
        Challenge Get(int id);
        List<Challenge> GetAll();
    }
}
