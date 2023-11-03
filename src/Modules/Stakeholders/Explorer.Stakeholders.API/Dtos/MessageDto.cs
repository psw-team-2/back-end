using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public MessageStatus Status { get; set; }
    }
}

public enum MessageStatus
{
    Unread,
    Read
}