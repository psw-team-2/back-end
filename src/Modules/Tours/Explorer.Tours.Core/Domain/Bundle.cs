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
        public List<Tour> Tours { get; init; }

        public BundleStatus Status { get; set; }

        public Bundle(long userId, string name, double price, BundleStatus status)
        {
            UserId = userId;
            Name = name;
            Price = price;
            Tours = new List<Tour>();
            Status = status;
        }

        public void AddTour(Tour tour)
        {
            if (Tours != null)
            {
                Tours.Add(tour);
            }
        }

        public void RemoveItem(Tour tour)
        {
            if (Tours != null)
            {
                Tours.Remove(tour);
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
