using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubRequest : Entity
    {
        public long ClubId { get; private set; }
        public long AccountId { get; private set; } 
        public RequestStatusEnum RequestStatus { get; private set; }
        public RequestTypeEnum RequestType { get; private set; }
       
        public ClubRequest(long clubId, long accountId, RequestStatusEnum requestStatus, RequestTypeEnum requestType) 
        {
            ClubId = clubId;
            AccountId = accountId;
            RequestStatus = requestStatus;
            RequestType = requestType;
            Validate();
        }

        private void Validate() 
        { 
        }
    }

    public enum RequestStatusEnum 
    { 
        Pending,
        Accepted,
        Rejected
    }

    public enum RequestTypeEnum
    {
        Invitation,
        Request
    }
}
