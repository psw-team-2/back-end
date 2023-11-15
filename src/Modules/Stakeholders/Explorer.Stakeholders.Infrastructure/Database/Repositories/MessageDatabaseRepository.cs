using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageDatabaseRepository : IMessageRepository
    {
        private readonly StakeholdersContext _dbContext;

        public MessageDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Message> GetAll()
        {
            return _dbContext.Messages.ToList();
        }
    }
}
