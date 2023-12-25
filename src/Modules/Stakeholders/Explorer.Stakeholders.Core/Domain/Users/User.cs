using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain.Users;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public string Email { get; init; }

    public string Token { get; init; }

    public User(string username, string password, UserRole role, bool isActive, string email, string token)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Email = email;
        Token = token;
        
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}