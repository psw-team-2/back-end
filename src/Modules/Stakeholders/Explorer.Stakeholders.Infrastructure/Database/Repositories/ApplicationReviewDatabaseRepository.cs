using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ApplicationReviewDatabaseRepository : IApplicationReviewRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ApplicationReviewDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
