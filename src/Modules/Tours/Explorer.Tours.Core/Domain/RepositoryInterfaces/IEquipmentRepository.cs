using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IEquipmentRepository
    {
        IEnumerable<Equipment> GetAll();

        public PagedResult<Equipment> GetEquipmentByIdsPaged(List<int> equipmentIds, int page, int pageSize);
        public List<Equipment> GetEquipmentByIdsList(List<int> equipmentIds);
    }
}
