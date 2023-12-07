using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubMessage : Entity
    {
        public long UserId { get; private set; }
        public long ClubId { get; private set; }
        public DateTime Time { get; private set; }
        public string Text { get; private set; }

        [JsonConstructor]
        public ClubMessage(long userId, long clubId, DateTime time, string text)
        {
            UserId = userId;
            ClubId = clubId;
            Time = time;
            Text = text;
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (ClubId == 0) throw new ArgumentException("Invalid ClubId");
            if (Text == null) throw new ArgumentException("Invalid Text");
        }

        
    }
}
