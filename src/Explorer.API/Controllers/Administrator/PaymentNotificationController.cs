using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/administrator/paymentNotification")]
    [ApiController]
    public class PaymentNotificationController : BaseApiController
    {
        private readonly IPaymentNotificationService _paymentNotificationService;
        public PaymentNotificationController(IPaymentNotificationService paymentNotificationService)
        {
            _paymentNotificationService = paymentNotificationService;
        }

        [HttpPost]
        public ActionResult<PaymentNotificationDto> Create([FromBody] PaymentNotificationDto paymentNotificationDto)
        {
            throw new NotImplementedException();
        }


        [HttpGet("unread-notifications/{profileId:int}")]
        public ActionResult<PagedResult<PaymentNotificationDto>> GetUnreadPaymentNotifications([FromQuery] int page, [FromQuery] int pageSize, long profileId)
        {
            var result = _paymentNotificationService.GetUnreadPaymentNotifications(page, pageSize, profileId);
            return CreateResponse(result);
        }
    }
}
