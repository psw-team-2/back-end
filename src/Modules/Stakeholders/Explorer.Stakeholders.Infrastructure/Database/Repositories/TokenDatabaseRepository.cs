using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TokenDatabaseRepository : ITokenRepository
    {
        private readonly StakeholdersContext _dbContext;

        public TokenDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Token Create(Token token)
        {
            _dbContext.Tokens.Add(token);
            _dbContext.SaveChanges();

            return token;
        }

        public Token Get(string value)
        {
            return _dbContext.Tokens.SingleOrDefault(u => u.Value == value);
        }
    }
}
