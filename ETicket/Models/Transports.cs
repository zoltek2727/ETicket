using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Transports
    {
        public Transports()
        {
            TransportReservations = new HashSet<TransportReservations>();
        }

        public int TransportId { get; set; }
        public string TransportName { get; set; }
        public string TransportDescription { get; set; }

        public ICollection<TransportReservations> TransportReservations { get; set; }
    }
}
