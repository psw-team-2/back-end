using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AnswerRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Answer GetAnswerByQuestionId(int questionId)
        {
            return _dbContext.Answers.FirstOrDefault(a => a.QuestionId == questionId);
        }
    }
}
