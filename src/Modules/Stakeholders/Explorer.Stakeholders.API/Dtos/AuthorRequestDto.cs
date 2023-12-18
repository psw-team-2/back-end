using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class AuthorRequestDto
    {
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }

    public enum RequestStatus
    {
        UnderReview,
        Accepted,
        Declined
    }
}
