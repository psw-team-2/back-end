using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubRequestDto
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int TouristId { get; set; }
        public RequestStatusEnum RequestStatus { get; set; }
        public RequestTypeEnum RequestType { get; set; }
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
