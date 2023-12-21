using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        public PagedResult<CheckPoint> GetCheckpointsByIdsPaged(List<int> checkpointIds, int page, int pageSize);
    }
}