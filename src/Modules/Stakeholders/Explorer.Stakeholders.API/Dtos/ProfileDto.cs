

namespace Explorer.Stakeholders.API.Dtos;

public class ProfileDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string Motto { get; set; }
    public long UserId { get; set; }
    public bool IsActive { get; set; }
    public List<FollowDto> Follows { get; set; }
    public TourPreferenceDto TourPreference { get; set; }
}