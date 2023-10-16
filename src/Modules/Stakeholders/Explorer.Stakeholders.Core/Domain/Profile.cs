using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain;

public class Profile : Entity
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfilePicture { get; init; }
    public string Biography { get; init; }
    public string Motto { get; init; }

    public Profile(string firstName, string lastName, string profilePicture, string biography, string motto)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
        Biography = biography;
        Motto = motto;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName)) throw new ArgumentException("Invalid FirstName");
        if (string.IsNullOrWhiteSpace(LastName)) throw new ArgumentException("Invalid LastName");
        if (string.IsNullOrWhiteSpace(ProfilePicture)) throw new ArgumentException("Invalid ProfilePicture");
        if (string.IsNullOrWhiteSpace(Biography)) throw new ArgumentException("Invalid Biography");
        if (string.IsNullOrWhiteSpace(Motto)) throw new ArgumentException("Invalid Motto");
    }
}