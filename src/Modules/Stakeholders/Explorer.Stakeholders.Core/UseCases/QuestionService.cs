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
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class QuestionService : CrudService<QuestionDto, Question>, IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public QuestionService(ICrudRepository<Question> repository, IMapper mapper, IQuestionRepository questionRepository, IUserRepository userRepository, IEmailService emailService) : base(repository, mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public override Result<QuestionDto> Create(QuestionDto questionDto)
        {

            questionDto.isAnswered = false;

            var result = base.Create(questionDto);

            if (result.IsSuccess)
            {
                _emailService.SendEmailToAdmins(questionDto);
            }

            return result;
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

        public Result<string> GetQuestionTextByQuestionId(int questionId)
        {
            try
            {
                var question = _questionRepository.GetQuestionById(questionId);

                if (question != null)
                {
                    return Result.Ok(question.Text);
                }
                else
                {
                    return Result.Fail<string>("Question not found");
                }
            }
            catch (Exception e)
            {
                return Result.Fail<string>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
