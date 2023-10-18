using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain;


public class MockTour : Entity
{
    public string? TourInfo { get; init; }

    public MockTour(string tourInfo)
    {

        //Temporarily check description, could allow empty description in future
        if (string.IsNullOrWhiteSpace(tourInfo)) throw new ArgumentException("Invalid Tour Info.");
        TourInfo = tourInfo;
    }
}