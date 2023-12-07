using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterDatabaseRepository : IEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Encounter Get(int id)
        {
            return _dbContext.Challenges.FirstOrDefault(u => u.Id == id);
        }

        public List<Encounter> GetAll()
        {
            return _dbContext.Challenges.ToList();
        }
    }
}
