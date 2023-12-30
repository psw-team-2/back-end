using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AuthorRequestDatabaseRepository : IAuthorRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AuthorRequestDatabaseRepository(StakeholdersContext dbContext) {
            _dbContext = dbContext;
        }

        public List<AuthorRequest> GetAll()
        {
            return _dbContext.AuthorRequests.ToList();
        }
    }
}
