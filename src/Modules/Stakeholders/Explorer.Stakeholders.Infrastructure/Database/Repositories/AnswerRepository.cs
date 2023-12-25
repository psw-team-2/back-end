using Explorer.Stakeholders.API.Dtos;
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

        public IEnumerable<AnswerDto> GetAnswersByCategoryWithQuestionText(Core.Domain.AnswerCategory category)
        {
            var answers = _dbContext.Answers
                .Where(a => a.Category == category && a.Visability)
                .ToList();

            var answerDtos = answers.Select(answer => new AnswerDto
            {
                Id = answer.Id,
                TouristId = answer.TouristId,
                AdminId = answer.AdminId,
                Text = answer.Text,
                Category = (API.Dtos.AnswerCategory)answer.Category,
                Visability = answer.Visability,
                QuestionId = answer.QuestionId,
                QuestionText = _dbContext.Question
                    .Where(q => q.Id == answer.QuestionId)
                    .Select(q => q.Text)
                    .FirstOrDefault()
            });

            return answerDtos.ToList();
        }
    }
}
