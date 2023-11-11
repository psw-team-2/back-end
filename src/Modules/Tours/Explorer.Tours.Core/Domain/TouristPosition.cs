using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TouristPosition : Entity
    {
        public long UserId { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public TouristPosition(long userId, double latitude, double longitude)
        {
            if ((latitude <= -90 && latitude >= 90) || double.IsNaN(latitude)) throw new ArgumentException("Invalid latitude.");
            if ((longitude <= -90 && longitude >= 90) || double.IsNaN(longitude)) throw new ArgumentException("Invalid longitude.");
            UserId = userId;
            Latitude = latitude;                
            Longitude = longitude;
        }
    }
}
