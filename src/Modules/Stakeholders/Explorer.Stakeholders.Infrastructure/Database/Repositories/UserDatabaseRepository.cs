using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

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
    }
}