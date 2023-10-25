using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Data;

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

    public long GetHighestUserId()
    {
        long highestUserId = _dbContext.Users
            .OrderByDescending(user => user.Id)
            .Select(user => user.Id)
            .FirstOrDefault();

        return highestUserId;
    }

    public User Create(User user)
    {
        user.Id = GetHighestUserId()+1;
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


    public User Get(int id)
    {
        return _dbContext.Users.FirstOrDefault(i => i.Id == id);
    }

    public List<long> GetAllUserIds()
    {
        return _dbContext.Users.Select(user => user.Id).ToList();

public Result<object> GetUserById(long userId)
    {
        try
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var credentialsDto = new CredentialsDto
                {
                    Username = user.Username,
                    Password = user.Password,
                };

                return Result.Ok((object)credentialsDto);
            }
            else
            {
                return Result.Fail("User not found.");
            }
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error: {ex.Message}");
        }

    }
}