using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class AuthorRequest : Entity
    {
        public long ProfileId { get; init; }
        public RequestStatus RequestStatus { get; init; }
    }
}

public enum RequestStatus
{
    UnderReview,
    Accepted,
    Declined
}
