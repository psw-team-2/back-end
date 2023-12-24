using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class QuestionService : CrudService<QuestionDto, Question>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public QuestionService(ICrudRepository<Question> repository, IMapper mapper, IQuestionRepository questionRepository) : base(repository, mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public override Result<QuestionDto> Create(QuestionDto questionDto)
        {
            questionDto.isAnswered = false;
            return base.Create(questionDto);
        }

        public Result<IEnumerable<QuestionDto>> GetAllUnanswered()
        {
            IEnumerable<Question> questions = _questionRepository.GetAllUnanswered();
            IEnumerable<QuestionDto> questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Result.Ok(questionDtos);

        }

        public Result<IEnumerable<QuestionDto>> GetAllAnswered()
        {
            IEnumerable<Question> questions = _questionRepository.GetAllAnswered();
            IEnumerable<QuestionDto> questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Result.Ok(questionDtos);

        }


    }
}
