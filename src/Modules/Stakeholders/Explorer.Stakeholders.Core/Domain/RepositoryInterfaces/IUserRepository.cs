using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);

    User GetUserByToken(string token);
    User Create(User user);
    long GetPersonId(long userId);
    User Get(int id);
    List<long> GetAllUserIds();
    Result<object> GetUserById(long userId);
    public Result GetUserById(int userId);
    User Update(User user);
    string GetTouristEmail(int id);
    User? GetByEmail(string email);
    List<string> GetAdminEmails();
    public string GetUserEmail(long userId);
    List<User> GetAuthors();
}