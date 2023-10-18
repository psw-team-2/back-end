using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain;


public class TourProblem : Entity
{
    public string? ProblemCategory { get; init; }
    public string? ProblemPriority { get; init; }
    public string? Description { get; init; }
    public DateTime TimeStamp { get; init; }
    public long MockTourId { get; init; }

    public TourProblem(string? problemCategory, string? problemPriority,
        string? description, DateTime timeStamp, long mockTourId)
    {
        ProblemCategory = problemCategory;
        ProblemPriority = problemPriority;
        Description = description;
        TimeStamp = timeStamp;
        MockTourId = mockTourId;
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
        if (string.IsNullOrWhiteSpace(ProblemCategory)) throw new ArgumentNullException("Problem Category is empty");
        if (string.IsNullOrWhiteSpace(ProblemPriority)) throw new ArgumentException("Problem Priority is empty");
        if (TimeStamp == null) throw new ArgumentException("Time Stamp is empty");
        if(MockTourId == 0) throw new ArgumentException("Invalid MockTourId");
    }
}