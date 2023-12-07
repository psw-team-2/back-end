using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubMessageDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ClubId { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
