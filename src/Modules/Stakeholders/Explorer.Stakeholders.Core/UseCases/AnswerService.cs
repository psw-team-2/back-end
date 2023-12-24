using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AnswerService : CrudService<AnswerDto, Answer>, IAnswerService
    {
        public AnswerService(ICrudRepository<Answer> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
