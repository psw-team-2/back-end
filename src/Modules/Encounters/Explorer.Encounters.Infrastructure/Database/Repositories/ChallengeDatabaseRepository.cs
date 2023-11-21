using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class ChallengeDatabaseRepository : IChallengeRepository
    {
        private readonly EncountersContext _dbContext;

        public ChallengeDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Challenge Get(int id)
        {
            return _dbContext.Challenges.FirstOrDefault(u => u.Id == id);
        }

        public List<Challenge> GetAll()
        {
            return _dbContext.Challenges.ToList();
        }
    }
}
