using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Models
{
    public class PhotoEvents
    {
        public int PhotoEventId { get; set; }
        public bool PhotoEventDefault { get; set; }
        public int PhotoId { get; set; }
        public int EventId { get; set; }

        public Photos Photo { get; set; }
        public Events Event { get; set; }
    }
}
