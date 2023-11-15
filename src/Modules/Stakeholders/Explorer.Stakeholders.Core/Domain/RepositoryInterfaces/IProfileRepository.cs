using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IProfileRepository
{
    //bool Exists(string username);
    //User? GetActiveByName(string username);
    Profile Create(Profile profile);
    List<Profile> GetAll();
    Profile Get(int id);
    Profile Update(Profile profile);
}