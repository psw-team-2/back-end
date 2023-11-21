using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain
{
    public class Challenge : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public int XP { get; init; }
        public Status Status { get; init; }
        public Type Type { get; init; }

        public Challenge(string name, string description, double latitude, double longitude, int xP, Status status, Type type)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            XP = xP;
            Status = status;
            Type = type;
        }
    }

    public enum Status
    {
        Active,
        Draft,
        Archived
    }

    public enum Type
    {
        Social,
        Location,
        Misc
    }
}
