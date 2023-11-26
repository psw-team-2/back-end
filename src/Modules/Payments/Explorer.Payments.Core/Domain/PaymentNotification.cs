using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class PaymentNotification : Entity
    {
        public long AdministratorId { get; init; }
        public long UserId { get; init; }
        public int AdventureCoin { get; init; } 
        public NotificationStatus Status { get; init; }

        public PaymentNotification(long administratorId, long userId, int adventureCoin, NotificationStatus status)
        {
            administratorId = AdministratorId;
            userId = UserId;
            adventureCoin = AdventureCoin;
            status = Status;
        }

        public enum NotificationStatus
        {
            Unread,
            Read
        }


    }
}
