using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{

    public class TourCompositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountStatus Status { get; set; }
        public List<int>? ToursId { get; set; }
        public int Difficulty { get; set; }
        public List<string>? Tags { get; set; }
        public List<int>? Equipment { get; set; }
        public List<long>? CheckPoints { get; set; }
        public List<long>? Objects { get; set; }
        public double FootTime { get; set; }
        public double BicycleTime { get; set; }
        public double CarTime { get; set; }
        public double TotalLength { get; set; }
        public DateTime PublishTime { get; set; }
        public long AuthorId { get; set; }
    }
}
