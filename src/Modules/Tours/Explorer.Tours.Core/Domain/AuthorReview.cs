using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class AuthorReview: Entity
    {
        public int Grade { get; init; }
        public string Comment { get; init; }
        public long AuthorId { get; init; }
        public DateTime ReviewDate { get; init; }
        public long TouristId { get; init; } 
        public bool IsApproved { get; private set; }

        public AuthorReview(int grade, string comment, long authorId, DateTime reviewDate, long touristId)
        {
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Comment is empty.");
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Invalid grade");
            }

            Grade = grade;
            Comment = comment;
            AuthorId = authorId;
            ReviewDate = reviewDate;
            TouristId = touristId;
            IsApproved = true;
        }

        public void SetIsApproved(bool value)
        {
            IsApproved = value;
        }
    }
}
