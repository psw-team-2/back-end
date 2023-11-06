using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Microsoft.AspNetCore.Mvc.Route("api/administration/message2")]
    public class MessageContoller2 : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessageContoller2(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public ActionResult<MessageDto> Create([FromBody] MessageDto message)
        {
            var result = _messageService.Create(message);
            return CreateResponse(result);
        }

        [HttpGet("unread-messages/{profileId:int}")]
        public ActionResult<PagedResult<MessageDto>> GetUnreadMessages([FromQuery] int page, [FromQuery] int pageSize, long profileId)
        {
            var result = _messageService.GetUnreadMessages(page, pageSize, profileId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/{senderId:int}/{receiverId:int}")]
        public ActionResult<MessageDto> Update(int id, int senderId, int receiverId, [FromBody] MessageDto message)
        {
            message.Id = id;
            message.SenderId = senderId;
            message.ReceiverId = receiverId;

            var result = _messageService.Update(message);
            return CreateResponse(result);
        }
    }
}
