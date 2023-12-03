namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int XP { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
        public bool Mandatory { get; set; }
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
