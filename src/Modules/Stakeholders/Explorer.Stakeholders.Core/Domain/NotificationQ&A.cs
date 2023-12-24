using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class NotificationQ_A : Entity
    {
        public long UserId { get; init; }
        public NotificationStatus Status { get; init; }

        public NotificationQ_A(long userId, NotificationStatus status)
        {
            UserId = userId;
            Status = status;
        }
    }

    public enum NotificationStatus
    {
        Unread,
        Read
    }
}
