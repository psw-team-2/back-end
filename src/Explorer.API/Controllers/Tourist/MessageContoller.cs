using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Microsoft.AspNetCore.Mvc.Route("api/administration/message")]
    public class MessageContoller : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessageContoller(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public ActionResult<MessageDto> Create([FromBody] MessageDto message)
        {
            var result = _messageService.Create(message);
            return CreateResponse(result);
        }
    }
}
