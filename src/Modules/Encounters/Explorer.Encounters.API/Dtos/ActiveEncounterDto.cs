namespace Explorer.Encounters.API.Dtos
{
    public class ActiveEncounterDto
    {
        public long Id { get; set; }
        public long EncounterId { get; set; }
        public long TouristId { get; set; }
        public State State { get; set; }
        public DateTime? End { get; set; }
    }
}

public enum State
{
    Activated,
    Done,
    Abandoned
}