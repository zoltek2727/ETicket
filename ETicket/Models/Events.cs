using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Events
    {
        public Events()
        {
            Tickets = new HashSet<Tickets>();
            PhotoEvents = new HashSet<PhotoEvents>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime? EventEnd { get; set; }
        public int EventTicketPurchaseLimit { get; set; }
        public int PlaceId { get; set; }
        public int? TourId { get; set; }

        public Places Place { get; set; }
        public Tours Tour { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
        public ICollection<PhotoEvents> PhotoEvents { get; set; }
    }
}
