using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class GiftcardDto
    {
        public int AC { get; set; }
        public string Note { get; set; }
        public int RecommendedTour { get; set; }
        public int Receiver { get; set; }
        public int SenderId { get; set; }
        public string Sender { get; set; }
    }
}
