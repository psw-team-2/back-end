using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ApplicationReview : Entity
    {
        public int Grade { get; init; }
        public DateTime TimeStamp { get; init; }
        public long UserId { get; init; }
        public string Comment { get; init; }

        public ApplicationReview(int grade, DateTime timeStamp, long userId, string comment)
        {
            Grade = grade;
            TimeStamp = timeStamp;
            UserId = userId;
            Comment = comment;
        }
    }
}
