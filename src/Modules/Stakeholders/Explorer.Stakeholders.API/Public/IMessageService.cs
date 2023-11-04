using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IMessageService
    {
        Result<MessageDto> Create(MessageDto message);
        Result<PagedResult<MessageDto>> GetUnreadMessages(int page, int pageSize, long profileId);
        Result<MessageDto> Update(MessageDto message);
    }
}
