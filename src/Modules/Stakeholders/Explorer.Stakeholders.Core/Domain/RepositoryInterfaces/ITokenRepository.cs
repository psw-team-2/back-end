using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITokenRepository
    {
        Token Create(Token token);
        Token Get(string value);
    }
}
