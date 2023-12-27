using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TokenService : CrudService<TokenDto, Token>, ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ICrudRepository<Token> repository, IMapper mapper, ITokenRepository tokenRepository) : base(repository, mapper)
        {
            _tokenRepository = tokenRepository;
        }

        public Result<TokenDto> Create(TokenDto token)
        {
            try
            {
                var request = _tokenRepository.Create(MapToDomain(token));

                if (request == null)
                {
                    return Result.Fail<TokenDto>(FailureCode.NotFound).WithError("Something wrong lol");
                }

                var requestDto = MapToDto(request);
                return Result.Ok(requestDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TokenDto> Get(string value)
        {
            try
            {
                var request = _tokenRepository.Get(value);

                if (request == null)
                {
                    return Result.Fail<TokenDto>(FailureCode.NotFound).WithError("Request not found");
                }

                var requestDto = MapToDto(request);
                return Result.Ok(requestDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
