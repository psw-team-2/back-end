using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        public long TourId { get; init; }
        public long UserId { get; init; }
        public DateTime PurchaseDate { get; init; }

        public TourPurchaseToken(long tourId, long userId, DateTime purchaseDate)
        {
            TourId = tourId;
            UserId = userId;
            PurchaseDate = purchaseDate;
        }
    }
}
