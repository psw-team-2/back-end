using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IQuestionService
    {
        Result<QuestionDto> Create(QuestionDto questionDto);
        Result<PagedResult<QuestionDto>> GetPaged(int page, int pageSize);
        Result<IEnumerable<QuestionDto>> GetAllUnanswered();
        Result<IEnumerable<QuestionDto>> GetAllAnswered();
    }
}
