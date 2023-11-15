using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourProblemResponseDto
    {
        public int Id { get; set; }
        public string? Response { get; set; }
        public DateTime TimeStamp { get; init; }
        public long TourProblemId { get; set; }
        public long CommenterId { get; set; }
    }
}
