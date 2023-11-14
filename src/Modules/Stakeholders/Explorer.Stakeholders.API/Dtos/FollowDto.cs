using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowDto
    {
        public long ProfileId { get; set; }
        public long FollowerId { get; set;}
    }
}
