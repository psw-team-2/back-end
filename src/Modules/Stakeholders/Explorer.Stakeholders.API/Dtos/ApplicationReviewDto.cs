using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ApplicationReviewDto
    {
        public int Grade { get; set; }
        public DateTime TimeStamp { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }
    }
}
