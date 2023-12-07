using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain
{
    public class Encounter : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public int XP { get; init; }
        public Status Status { get; init; }
        public Type Type { get; init; }
        public bool Mandatory { get; init; }
        public int PeopleCount { get; init; }
        public float Range { get; init; }
        public string Image { get; init; }


        public Encounter(string name, string description, double latitude, double longitude, int xP, Status status, Type type, bool mandatory, int peopleCount, float range, string image)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            XP = xP;
            Status = status;
            Type = type;
            Mandatory = mandatory;
            PeopleCount = peopleCount;
            Range = range;
            Image = image;
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
