using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountStatus Status { get; set; }
        public int Difficulty { get; set; }
        public PriceDto Price { get; set; }
        public List<String>? Tags { get; set; }

    }
}
