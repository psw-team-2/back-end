using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain.Blog
{
    public class UserBlogTourReport : ValueObject
    {
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Length { get; set; }
        public List<int> Equipment { get; set; }
        public List<int> CheckpointsVisited { get; set; }


        [JsonConstructor]
        public UserBlogTourReport(int tourId, DateTime startTime, DateTime endTime, double length)
        {
            TourId = tourId;
            StartTime = startTime;
            EndTime = endTime;
            Length = length;
            Equipment = new List<int>();
            CheckpointsVisited = new List<int>();
            Validate();
        }

        private void Validate()
        {
            if (Length < 0) throw new ArgumentException("Invalid Length");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TourId;
            yield return StartTime;
            yield return EndTime;
            yield return Length;
            yield return Equipment;
            yield return CheckpointsVisited;
        }

        public bool IsStartTimeBeforeEndTime()
        {
            return StartTime < EndTime;
        }


    }
}
