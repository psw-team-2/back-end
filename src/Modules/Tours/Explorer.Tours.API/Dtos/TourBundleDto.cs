using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourBundleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountStatus Status { get; set; }
        public int Difficulty { get; set; }
        public double Price { get; set; }
        public double FootTime { get; set; }
        public double BicycleTime { get; set; }
        public double CarTime { get; set; }
        public double TotalLength { get; set; }
        public DateTime PublishTime { get; set; }
        public long AuthorId { get; set; }

        public string Image { get; set; }
    }
}
