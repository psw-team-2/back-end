using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class AuthorReviewDto
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public long AuthorId { get; set; }
        public DateTime ReviewDate { get; set; }
        public long TouristId { get; set; }
        public bool IsApproved { get; set;}
    }
}
