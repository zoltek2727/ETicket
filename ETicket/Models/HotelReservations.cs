using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class HotelReservations
    {
        public HotelReservations()
        {
            Events = new HashSet<Events>();
            Purchases = new HashSet<Purchases>();
        }

        public int HotelReservationId { get; set; }
        public DateTime HotelReservationStart { get; set; }
        public DateTime HotelReservationEnd { get; set; }
        public int EventId { get; set; }
        public int RoomId { get; set; }

        public Events Event { get; set; }
        public Rooms Room { get; set; }
        public ICollection<Events> Events { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
