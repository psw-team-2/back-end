using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class TourReview : Entity
    {
        public int Grade { get; init; }
        public string Comment { get; init; }

        public long UserId { get; init; }
        public DateTime VisitDate { get; init; }
        public DateTime ReviewDate { get; init; }
        public string Images { get; init; } //proveri

        public TourReview(int grade, string comment, long userId, DateTime visitDate, DateTime reviewDate, string images)
        {
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Invalid comment.");
            if (string.IsNullOrWhiteSpace(images)) throw new ArgumentException("Invalid images.");
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Invalid grade");
            }

            Grade = grade;
            Comment = comment;
            UserId = userId;
            VisitDate = visitDate;
            ReviewDate = reviewDate;
            Images = images;
        }

    }
}
