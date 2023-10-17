using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class CheckPoint : Entity
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    //public string Image {get; private set;}

    public CheckPoint(double latitude, double longitude, string name, string description)
    {
        if ((latitude <= -90 && latitude >= 90) || double.IsNaN(latitude)) throw new ArgumentException("Invalid latitude.");
        if ((longitude <= -90 && longitude >= 90) || double.IsNaN(longitude)) throw new ArgumentException("Invalid longitude.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        //validacija za sliku
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        //Image = image
    }
}
