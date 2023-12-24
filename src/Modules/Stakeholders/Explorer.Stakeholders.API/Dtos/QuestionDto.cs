using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long AdminId { get; set; }
        public string Text { get; set; }

    }
}
