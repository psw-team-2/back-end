using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubMessageService : CrudService<ClubMessageDto, ClubMessage>, IClubMessageService
    {
        public ClubMessageService(ICrudRepository<ClubMessage> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public Result<PagedResult<ClubMessageDto>> GetByClubId(int clubId, int page, int pageSize)
        {
            try
            {
                var pagedResult = GetPaged(page, pageSize);
                if (pagedResult != null)
                {

                    var filteredClubMessages = pagedResult.Value.Results.Where(x => x.ClubId == clubId).ToList();

                    var filteredClubMessagesPagedResult = new PagedResult<ClubMessageDto>(
                        filteredClubMessages,
                        filteredClubMessages.Count
                    );

                    return Result.Ok(filteredClubMessagesPagedResult);
                }
                return Result.Fail("Club Message pagedResult is null");
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
