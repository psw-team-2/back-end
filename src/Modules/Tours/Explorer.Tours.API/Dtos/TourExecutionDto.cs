using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourExecutionDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Completed { get; set; }
        public bool Abandoned { get; set; }
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public List<int>? VisitedCheckpoints { get; set; }
        public DateTime LastActivity { get; set; }
        public double TouristDistance { get; set; }

    }
}
