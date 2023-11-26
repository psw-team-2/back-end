using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class PurchaseReport : Entity
    {
        public long UserId { get; init; }
        public long TourId { get; init; }
        public double AdventureCoin { get; set; }

        public PurchaseReport(long userId, long tourId, double adventureCoin)
        {
            UserId = userId;
            TourId = tourId;
            AdventureCoin = adventureCoin;
        }

    }
}
