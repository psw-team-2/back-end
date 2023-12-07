namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        Encounter Get(int id);
        List<Encounter> GetAll();
    }
}
