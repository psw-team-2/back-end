using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ProfileDatabaseRepository : IProfileRepository
{
    private readonly StakeholdersContext _dbContext;

    public ProfileDatabaseRepository(StakeholdersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Profile> GetAll()
    {
        return _dbContext.Profiles.ToList();
    }

    /*
    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }
    */

    public long GetHighestProfileId()
    {
        long highestProfileId = _dbContext.Profiles
            .OrderByDescending(profile => profile.Id)
            .Select(profile => profile.Id)
            .FirstOrDefault();

        return highestProfileId;
    }

    public Profile Create(Profile profile)
    {
        profile.Id = GetHighestProfileId() + 1;
        _dbContext.Profiles.Add(profile);
        _dbContext.SaveChanges();
        return profile;
    }

    /*
    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }
    */
}