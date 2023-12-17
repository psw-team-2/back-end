using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public abstract class EventSourcingAggregate : Entity
    {
        public List<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }

        public EventSourcingAggregate() {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);

    }
}
