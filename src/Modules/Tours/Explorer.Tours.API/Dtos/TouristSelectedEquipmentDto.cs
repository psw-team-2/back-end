using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TouristSelectedEquipmentDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long EquipmentId { get; set; }
    }
}
