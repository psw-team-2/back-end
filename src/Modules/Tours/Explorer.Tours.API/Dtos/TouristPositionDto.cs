namespace Explorer.Tours.API.Dtos
{
    public class TouristPositionDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
