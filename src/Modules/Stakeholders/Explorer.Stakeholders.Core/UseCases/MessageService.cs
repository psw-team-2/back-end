using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MessageService : CrudService<MessageDto, Message>, IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public MessageService(ICrudRepository<Message> repository, IMapper mapper, IMessageRepository messageRepository, IProfileService profileService) : base(repository, mapper)
        {
            _messageRepository = messageRepository;
            _profileService = profileService;
            _mapper = mapper;
        }
    }
}
