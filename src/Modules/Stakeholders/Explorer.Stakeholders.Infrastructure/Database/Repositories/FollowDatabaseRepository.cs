using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class FollowDatabaseRepository : IFollowRepository
    {
        private readonly StakeholdersContext _dbContext;

        public FollowDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Follow> GetAll()
        {
            return _dbContext.Follows.ToList();
        }
    }
}
