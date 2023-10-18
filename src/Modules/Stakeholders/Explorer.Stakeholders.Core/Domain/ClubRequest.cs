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
        public int ClubId { get; private set; }
        public int TouristId { get; private set; } 
        public RequestStatusEnum RequestStatus { get; private set; }
        public RequestTypeEnum RequestType { get; private set; }
       
        public ClubRequest(int clubId, int touristId, RequestStatusEnum requestStatus, RequestTypeEnum requestType) 
        {
            ClubId = clubId;
            TouristId = touristId;
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
