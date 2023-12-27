using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AnswerService : CrudService<AnswerDto, Answer>, IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly ICrudRepository<Question> _questionRepository;
        private IEmailService _emailService;
        public AnswerService(ICrudRepository<Answer> repository, IAnswerRepository answerRepository, ICrudRepository<Question> questionRepository, IEmailService emailService, IMapper mapper) : base(repository, mapper) 
        { 
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _emailService = emailService;
        }

        public Result<AnswerDto> Create(AnswerDto answerDto)
        {
            var question = _questionRepository.Get(answerDto.QuestionId);
            var result = base.Create(answerDto);
            if (result.IsSuccess) 
            {
                question.isAnswered = true;
                _questionRepository.Update(question);
                _emailService.SendEmailToUser(answerDto);
            }
            return result;
        }

        public Result<AnswerDto> GetAnswerByQuestionId(int questionId)
        {
            try
            {
                var answer = _answerRepository.GetAnswerByQuestionId(questionId);
                AnswerDto answerDto = MapToDto(answer);
                return Result.Ok(answerDto);
            }
            catch (Exception e)
            {
                return Result.Fail<AnswerDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<IEnumerable<AnswerDto>> GetAnswersByCategory(API.Dtos.AnswerCategory category)
        {
            try
            {
                var answers = _answerRepository.GetAnswersByCategoryWithQuestionText((Domain.AnswerCategory)category);
                return Result.Ok(answers);
            }
            catch (Exception e)
            {
                return Result.Fail<IEnumerable<AnswerDto>>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }



    }
}
