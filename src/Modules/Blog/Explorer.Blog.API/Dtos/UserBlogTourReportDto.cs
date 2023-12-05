using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class UserBlogTourReportDto
    {
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Length { get; set; }
        public List<int> Equipment { get; set; }
        public List<int> CheckpointsVisited { get; set; }
    }
}
