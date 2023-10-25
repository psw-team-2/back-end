using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public long BlogId { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModification { get; set; }
    }
}
