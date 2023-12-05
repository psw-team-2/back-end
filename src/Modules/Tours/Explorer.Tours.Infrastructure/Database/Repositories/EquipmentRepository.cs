using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Equipment> _dbSet;
        public EquipmentRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Equipment>();
        }

        public IEnumerable<Equipment> GetAll()
        {
            return _dbContext.Equipment.ToList();
        }

        public PagedResult<Equipment> GetEquipmentByIdsPaged(List<int> equipmentIds, int page, int pageSize)
        {
            var totalItems = _dbContext.Equipment.Count(e => equipmentIds.Contains((int)e.Id));
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var equipment = _dbContext.Equipment
                .Where(e => equipmentIds.Contains((int)e.Id))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedResult<Equipment>(
                    equipment,
                    equipment.Count
                );
        }

        public List<Equipment> GetEquipmentByIdsList(List<int> equipmentIds)
        {
            var equipment =  _dbContext.Equipment.Where(e => equipmentIds.Contains((int)e.Id)).ToList();
            return equipment;
        }
    }
}
