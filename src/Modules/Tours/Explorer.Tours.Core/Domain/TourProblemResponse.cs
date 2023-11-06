using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourProblemResponse: Entity
    {
        public string? Response { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public long TourProblemId { get; private set; }
        public long CommenterId { get; private set; }

        public TourProblemResponse(string? response, DateTime timeStamp, long tourProblemId, long commenterId)
        {
            Response = response;
            TimeStamp= timeStamp;
            TourProblemId = tourProblemId;
            CommenterId = commenterId;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Response)) throw new ArgumentNullException("Response is empty");
            if (TimeStamp == DateTime.MinValue) throw new ArgumentException("Time Stamp is empty");
            if (TourProblemId <= 0) throw new ArgumentException("Invalid TourProblemId");
            if (CommenterId <= 0) throw new ArgumentException("Invalid CommenterId");
        }
    }
}
