namespace Explorer.Tours.API.Dtos;

public enum TourProblemCategory { CATEGORY1, CATEGORY2 }
public enum TourProblemPriority { PRIORITY1, PRIORITY2 }

public class TourProblemDto
{
    public int Id { get; set; }
    public string? ProblemCategory { get; init; }
    public string? ProblemPriority { get; init; }
    public string? Description { get; set; }
    public DateTime TimeStamp { get; init; }
    public long TourId { get; set; }
    public bool IsClosed { get; set; }
    public bool IsResolved { get; set; }
    public long TouristId { get; set; }
}