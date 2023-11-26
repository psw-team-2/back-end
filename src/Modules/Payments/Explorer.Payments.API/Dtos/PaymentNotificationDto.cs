using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class PaymentNotificationDto
    {
        public int Id { get; set; }
        public int AdministratorId { get; set; }
        public int UserId { get; set; }
        public int AdventureCoin { get; set; }

        public NotificationStatus Status { get; set; }
    }
    public enum NotificationStatus
    {
        Unread,
        Read
    }
}
