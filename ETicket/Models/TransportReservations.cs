using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class TransportReservations
    {
        public TransportReservations()
        {
            Events = new HashSet<Events>();
            Purchases = new HashSet<Purchases>();
        }

        public int TransportReservationId { get; set; }
        public DateTime TransportReservationStart { get; set; }
        public DateTime? TransportReservationEnd { get; set; }
        public int TransportReservationPrice { get; set; }
        public string TransportReservationAddress { get; set; }
        public int TransportId { get; set; }
        public int EventId { get; set; }
        public int CityId { get; set; }

        public Cities City { get; set; }
        public Events Event { get; set; }
        public Transports Transport { get; set; }
        public ICollection<Events> Events { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
