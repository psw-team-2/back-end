using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class ActiveEncounter : Entity
    {
        public long EncounterId { get; init; }
        public long TouristId { get; init; }
        public State State { get; init; }
        public DateTime? End {  get; init; }
        public int XP { get; init; }
        public int Level { get; init; }

        public ActiveEncounter(long encounterId, long touristId, State state, DateTime? end, int xp, int level)
        {
            EncounterId = encounterId;
            TouristId = touristId;
            State = state;
            End = end;
            XP = xp;
            Level = level;
        }
    }
}
public enum State
{
    Activated,
    Done,
    Abandoned
}
