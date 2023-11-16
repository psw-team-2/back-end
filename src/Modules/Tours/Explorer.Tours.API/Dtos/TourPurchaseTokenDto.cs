using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public int Id { get; init; }
        public long UserId { get; init; }
        public int TourId { get; init; }
        public DateTime PurchaseDate { get; init; }
    }
}
