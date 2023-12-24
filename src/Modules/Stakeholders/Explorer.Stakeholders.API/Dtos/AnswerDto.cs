using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long AdminId { get; set; }
        public string Text { get; set; }
        public AnswerCategory Category { get; set; }
        public bool Visability { get; set; }
    }

    public enum AnswerCategory
    {
        Payment, Tour, TechnicalSupport, Other
    }
}
