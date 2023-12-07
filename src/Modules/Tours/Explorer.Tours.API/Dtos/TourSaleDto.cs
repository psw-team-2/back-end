using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourSaleDto
    {
        public int Id { get; set; }
        public List<long> TourIds { get; set; }
        public int AuthorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Discount { get; set; }
    }
}
