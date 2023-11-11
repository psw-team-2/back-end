using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Message : Entity
    {
        public long SenderId { get; init; }
        public long ReceiverId { get; init; }
        public string MessageContent { get; init; }
        public MessageStatus Status { get; init; }

        public Message(long senderId, long receiverId, string messageContent, MessageStatus status)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            MessageContent = messageContent;
            Status = status;
            Validate();
        }

        public void Validate()
        {
            if (SenderId == 0) throw new ArgumentException("Invalid SenderId");
            if (ReceiverId == 0) throw new ArgumentException("Invalid ReceiverId");
            if (string.IsNullOrWhiteSpace(MessageContent)) throw new ArgumentException("Invalid MessageContent");
        }
    }
}

public enum MessageStatus
{
    Unread,
    Read
}