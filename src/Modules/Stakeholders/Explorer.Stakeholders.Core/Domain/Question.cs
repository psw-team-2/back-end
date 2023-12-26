using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Question : Entity
    {
        public long TouristId { get; init; }
        public string Text { get; init; }

        public bool isAnswered { get; set; }
        public Question(long touristId, string text, bool isAnswered)
        {
            TouristId = touristId;
            Text = text;
            this.isAnswered = isAnswered;
        }
    }
}
