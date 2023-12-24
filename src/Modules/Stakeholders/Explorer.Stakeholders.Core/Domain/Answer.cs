using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Answer : Entity
    {
        public long TouristId { get; init; }
        public long AdminId { get; init; }
        public string Text { get; init; }
        public AnswerCategory Category { get; init; }
        public bool Visability { get; init; }
        public long QuestionId { get; init; }
        public Answer(long touristId, long adminId, string text, AnswerCategory category, bool visability, long questionId)
        {
            TouristId = touristId;
            AdminId = adminId;
            Text = text;
            Category = category;
            Visability = visability;
            QuestionId = questionId;
        }

    }

    public enum AnswerCategory
    {
        Payment, Tour, TechnicalSupport, Other
    }
}
