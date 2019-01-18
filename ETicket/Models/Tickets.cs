using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Tickets
    {
        public Tickets()
        {
            Purchases = new HashSet<Purchases>();
        }

        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public string TicketDescription { get; set; }
        public decimal TicketPrice { get; set; }
        public int EventId { get; set; }
        public int? SectorId { get; set; }

        public Events Event { get; set; }
        public Sectors Sector { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
