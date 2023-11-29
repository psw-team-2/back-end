using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Bundle : Entity
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public double Price { get; set; }

        public List<long> Tours { get; init; }

        public BundleStatus Status { get; init; }

        public Bundle(long userId, string name, double price, List<long> tours, BundleStatus status)
        {
            UserId = userId;
            Name = name;
            Price = price;
            Tours = tours;
            Status = status;
        }

        
    }

    public enum BundleStatus
    {
        Draft,
        Published,
        Archived
    }
}
