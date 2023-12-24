using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
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
        public AnswerService(ICrudRepository<Answer> repository, IAnswerRepository answerRepository, IMapper mapper) : base(repository, mapper) 
        { 
            _answerRepository = answerRepository;
        }

        public Result<AnswerDto> Create(AnswerDto answerDto)
        {
            var result = base.Create(answerDto);
            return result;
        }
    }
}
