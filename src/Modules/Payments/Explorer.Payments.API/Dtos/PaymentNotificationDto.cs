using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class PaymentNotificationDto
    {
        public long Id { get; set; }
        public long AdministratorId { get; set; }
        public long UserId { get; set; }
        public int AdventureCoin { get; set; }

        public NotificationStatus Status { get; set; }

        public PaymentNotificationDto() { }
    }
    public enum NotificationStatus
    {
        Unread,
        Read
    }
}
