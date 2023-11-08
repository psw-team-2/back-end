using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class RatingDto
    {
        public bool isUpvote { get; set; }
        public long UserId { get; set; }
        public long BlogId { get; set; }
    }
}
