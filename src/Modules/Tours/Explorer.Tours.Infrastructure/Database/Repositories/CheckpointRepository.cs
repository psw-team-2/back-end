using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class CheckpointRepository : ICheckpointRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<CheckPoint> _dbSet;

        public CheckpointRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<CheckPoint>();
        }

        public PagedResult<CheckPoint> GetCheckpointsByIdsPaged(List<int> checkpointIds, int page, int pageSize)
        {
            var totalItems = _dbContext.Equipment.Count(e => checkpointIds.Contains((int)e.Id));
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var checkpoints = _dbContext.CheckPoint
                .Where(e => checkpointIds.Contains((int)e.Id))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedResult<CheckPoint>(
                checkpoints,
                checkpoints.Count
            );

        }
    }
}