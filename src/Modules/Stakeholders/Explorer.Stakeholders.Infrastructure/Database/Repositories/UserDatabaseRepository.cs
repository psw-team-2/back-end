using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Net;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserDatabaseRepository : IUserRepository
{
    private readonly StakeholdersContext _dbContext;

    public UserDatabaseRepository(StakeholdersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }

    public User Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }

    public Result<object> GetUserById(long userId)
    {
        try
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                object credentialsDto = new CredentialsDto
                {
                    Username = user.Username,
                    // Other properties...
                };

                return Result.Ok(credentialsDto);
            }
            else
            {
                return Result.Fail("User not found.");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during database access
            return Result.Fail($"Error: {ex.Message}");
        }
    }

}