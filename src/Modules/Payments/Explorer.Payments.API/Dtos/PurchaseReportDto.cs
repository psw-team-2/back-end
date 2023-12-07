using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class PurchaseReportDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long TourId { get; set; }
        public double AdventureCoin { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
