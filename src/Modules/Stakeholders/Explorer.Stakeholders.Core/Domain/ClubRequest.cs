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
       // public Club Club { get; private set; }
        public User Account { get; private set; } 
        public RequestStatusEnum RequestStatus { get; private set; }
        public RequestTypeEnum RequestType { get; private set; }
       
        public ClubRequest(User account, RequestStatusEnum requestStatus, RequestTypeEnum requestType) 
        {
            Account = account;
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
