using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Events
    {
        public Events()
        {
            HotelReservations = new HashSet<HotelReservations>();
            Tickets = new HashSet<Tickets>();
            TransportReservations = new HashSet<TransportReservations>();
    }

    public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        public int EventTicketPurchaseLimit { get; set; }
        public int PlaceId { get; set; }
        public int? TourId { get; set; }
        public int? HotelReservationId { get; set; }
        public int? TransportReservationId { get; set; }

        public HotelReservations HotelReservation { get; set; }
        public Places Place { get; set; }
        public Tours Tour { get; set; }
        public TransportReservations TransportReservation { get; set; }
        public ICollection<HotelReservations> HotelReservations { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
        public ICollection<TransportReservations> TransportReservations { get; set; }
    }
}
