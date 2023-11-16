namespace Explorer.Tours.API.Dtos
{
    public class CheckpointVisitedDto
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public long CheckpointId { get;  set; }
        public DateTime Time { get; set; }
    }
}
