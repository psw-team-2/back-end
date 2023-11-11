using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserAccountAdministrationService : CrudService<UserAccountDto, User>, IUserAccountAdministrationService
    {
        private readonly IUserRepository _userRepository;

        public UserAccountAdministrationService(IUserRepository userRepository, ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) {
            _userRepository = userRepository;
        }

        
        public Result<User> Get(int id)
        {
            return _userRepository.Get(id);
        }
        
    }
}
