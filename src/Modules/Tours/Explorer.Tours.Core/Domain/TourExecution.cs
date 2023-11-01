using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourExecution : Entity
    {
        public int Id { get; private set; }
        public int TouristId { get; private set; }
        public int TourId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public bool Completed { get; private set; }
        public bool Abandoned { get; private set; }
        public double CurrentLatitude { get; private set; }
        public double CurrentLongitude { get; private set; }

        private TourExecution() { } 

        public TourExecution(int touristId, int tourId, DateTime startTime)
        {
            TouristId = touristId;
            TourId = tourId;
            StartTime = startTime;
            Completed = false;
            Abandoned = false;
        }
        public void UpdateFromDto(TourExecutionDto dto)
        {
            EndTime = dto.EndTime;
            Completed = dto.Completed;
            Abandoned = dto.Abandoned;
            CurrentLatitude = dto.CurrentLatitude;
            CurrentLongitude = dto.CurrentLongitude;
        }
    }
}
