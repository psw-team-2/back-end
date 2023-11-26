using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
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
            var result = _paymentNotificationService.Create(paymentNotificationDto);
            return CreateResponse(result);
        }
    }
}
