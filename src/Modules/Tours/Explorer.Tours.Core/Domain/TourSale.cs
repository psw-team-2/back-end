using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Explorer.Tours.Core.Domain
{
    public class TourSale : Entity
    {
        public List<long> TourIds { get; set; }
        public int AuthorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Discount { get; set; }

        public TourSale() { }

        public TourSale(List<long> tourIds, int authorId, DateTime startDate, DateTime endDate, int discount)
        {
            TourIds = tourIds;
            AuthorId = authorId;
            StartDate = startDate;
            EndDate = endDate;
            Discount = discount;
        }

        public void UpdateFromDto(TourSale dto)
        {
            TourIds = dto.TourIds;
            AuthorId = dto.AuthorId;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;
            Discount = dto.Discount;
        }

        public bool IsCreatedByUser(int userId)
        {
            return AuthorId == userId;
        }

    }
}
