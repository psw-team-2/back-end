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

        public Result<PagedResult<MessageDto>> GetUnreadMessages(int page, int pageSize, long profileId)
        {
            // Get all unread messages for the specified profile ID
            var unreadMessages = _messageRepository.GetAll()
                .Where(message => message.ReceiverId == profileId && message.Status == 0)
                .ToList();

            // Optionally, you can map the unread messages to MessageDto if needed.
            var unreadMessageDtos = _mapper.Map<List<MessageDto>>(unreadMessages);

            // Create a paged result based on your custom PagedResult class
            var pagedResult = Result.Ok(new PagedResult<MessageDto>(unreadMessageDtos, unreadMessageDtos.Count));

            return pagedResult;
        }
    }
}
