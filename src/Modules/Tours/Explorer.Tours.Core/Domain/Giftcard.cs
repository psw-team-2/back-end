using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Giftcard : Entity
    {
        public int AC { get; init; }
        public string Note { get; init; }
        public int RecommendedTour { get; init; }
        public long Receiver { get; init; }
        public long SenderId { get; init; }
        public string Sender { get; init; }

        public Giftcard(int aC, string note, int recommendedTour, long receiver, long senderId, string sender)
        {
            AC = aC;
            Note = note;
            RecommendedTour = recommendedTour;
            Receiver = receiver;
            SenderId = senderId;
            Sender = sender;
        }
    }
}
