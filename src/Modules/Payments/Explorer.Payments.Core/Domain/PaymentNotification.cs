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
        public long UserId { get; init; }
        public double AdventureCoin { get; init; } 
        public NotificationStatus Status { get; init; }

        public PaymentNotification(long id,  long userId, double adventureCoin, NotificationStatus status)
        {
            Id = id;
            UserId = userId;
            AdventureCoin = adventureCoin;
            Status = status;
        }

        public enum NotificationStatus
        {
            Unread,
            Read
        }


    }
}
