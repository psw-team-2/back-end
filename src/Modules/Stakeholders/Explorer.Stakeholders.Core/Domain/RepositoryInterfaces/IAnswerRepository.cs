using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IAnswerRepository
    {
        Answer GetAnswerByQuestionId(int questionId);
        IEnumerable<AnswerDto> GetAnswersByCategoryWithQuestionText(Core.Domain.AnswerCategory category);
    }
}
