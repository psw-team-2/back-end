using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class SecretRepository : ISecretRepository
    {
        private readonly ToursContext _dbContext;

        public SecretRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Secret GetByCheckPointId(int checkPointId)
        {
            return _dbContext.Secrets.FirstOrDefault(s => s.CheckPointId == checkPointId);
        }
    }
}
