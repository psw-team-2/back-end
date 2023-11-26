using Explorer.Payments.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class BundleDto
    {
        public int Id { get; set; }
        public int UserId { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }

        public BundleStatus Status { get; init; }

        public List<int> Tours { get; init; }

        public enum BundleStatus
        {
            Draft,
            Published,
            Archived
        }
    }

    
}
