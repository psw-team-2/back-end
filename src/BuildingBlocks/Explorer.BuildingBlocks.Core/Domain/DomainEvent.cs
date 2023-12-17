using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public abstract class DomainEvent
    {
        public Guid Id { get; private set; }

        public DomainEvent(Guid aggregateId) { 
            Id = aggregateId;
        }
        
    }
}
