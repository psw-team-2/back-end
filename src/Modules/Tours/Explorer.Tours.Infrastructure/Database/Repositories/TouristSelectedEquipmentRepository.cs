using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TouristSelectedEquipmentRepository : ITouristSelectedEquipmentRepository
    {
        private readonly ToursContext _dbContext;
        public TouristSelectedEquipmentRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TouristSelectedEquipment GetByTouristAndEquipment(long touristId, long equipmentId)
        {
            return _dbContext.TouristSelectedEquipment.FirstOrDefault(teq => teq.UserId == touristId && teq.EquipmentId == equipmentId);
        }
        
        
    }
}
