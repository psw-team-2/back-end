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
        public List<int> Tours { get; init; }

        public BundleStatus Status { get; set; }

        public Bundle(long userId, string name, double price, BundleStatus status, List<int> tours)
        {
            UserId = userId;
            Name = name;
            Price = price;
            Tours = tours;
            Status = status;
        }

        public void AddTour(int tourId)
        {
            if (Tours != null)
            {
                Tours.Add(tourId);
            }
        }

        public void RemoveItem(int tourId)
        {
            if (Tours != null)
            {
                Tours.Remove(tourId);
            }
        }


        public enum BundleStatus
        {
            Draft,
            Published,
            Archived
        }
    }
}
