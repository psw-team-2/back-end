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
        public long AdminId { get; init; }
        public string Text { get; init; }

        public Question(long touristId, long adminId, string text)
        {
            TouristId = touristId;
            AdminId = adminId;
            Text = text;
        }
    }
}
