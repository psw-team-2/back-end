namespace Explorer.Stakeholders.API.Dtos;

public class ProfileDto
{
    public long Id { get; set; } // dodato
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string Motto { get; set; }

}