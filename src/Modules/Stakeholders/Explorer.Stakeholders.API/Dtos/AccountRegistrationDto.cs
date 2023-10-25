using System.Security.Principal;

namespace Explorer.Stakeholders.API.Dtos;

public class AccountRegistrationDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set;}
    public string Motto { get; set; }

}