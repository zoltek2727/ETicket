using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Purchases
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseTicketDate { get; set; }
        public int PurchaseAmount { get; set; }
        public string Id { get; set; }
        public int TicketId { get; set; }
        public int DeliveryId { get; set; }

        public Deliveries Delivery { get; set; }
        public Tickets Ticket { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
