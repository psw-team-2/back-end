using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public List<int>? Items { get; set; }
    }
}
