﻿using Explorer.BuildingBlocks.Core.Domain;
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
        public TourExecution() { }

        public TourExecution(int touristId, int tourId, DateTime startTime, DateTime lastActivity, double touristDistance)
        {
            TouristId = touristId;
            TourId = tourId;
            StartTime = startTime;
            Completed = false;
            Abandoned = false;
            LastActivity = lastActivity;
            TouristDistance = touristDistance;  
        }
        public void UpdateFromDto(TourExecutionDto dto)
        {
            EndTime = dto.EndTime;
            Completed = dto.Completed;
            Abandoned = dto.Abandoned;
            CurrentLatitude = dto.CurrentLatitude;
            CurrentLongitude = dto.CurrentLongitude;
            VisitedCheckpoints = dto.VisitedCheckpoints;
            TouristDistance = dto.TouristDistance;
        }
    }
}
