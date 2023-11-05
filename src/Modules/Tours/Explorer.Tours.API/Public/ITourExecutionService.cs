using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourExecutionService
    {
        public Result<TourExecutionDto> StartTour(int touristId, int tourId, double startLatitude, double startLongitude);
        public Result<TourExecutionDto> CompleteTour(int tourExecutionId, double endLatitude, double endLongitude);
        public Result<TourExecutionDto> AbandonTour(int tourExecutionId, double abandonLatitude, double abandonLongitude);

    }
}
