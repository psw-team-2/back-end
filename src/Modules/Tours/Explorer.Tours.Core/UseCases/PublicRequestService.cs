using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicRequestService : CrudService<PublicRequestDto, PublicRequest>, IPublicRequestService
    {
        public PublicRequestService(ICrudRepository<PublicRequest> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<IEnumerable<PublicRequestDto>> GetPublicRequestsByUserId(int userId)
        {
            try
            {
                var pagedResult = CrudRepository.GetPaged(1, int.MaxValue);
                var responses = pagedResult.Results.Where(r => r.AuthorId == userId && r.IsNotified == false).ToList();

                if (responses.Count > 0)
                {
                    return Result.Ok(responses.Select(MapToDto));
                }
                else
                {
                    return Result.Ok<IEnumerable<PublicRequestDto>>(new List<PublicRequestDto>()).WithSuccess("No unnotified public requests.");
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail<IEnumerable<PublicRequestDto>>(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
