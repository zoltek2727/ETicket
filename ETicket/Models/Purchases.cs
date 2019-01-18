using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Purchases
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseTicketDate { get; set; }
        public string PurchaseSelectedRow { get; set; }
        public string PurchaseSelectedRowSeat { get; set; }
        public string Id { get; set; }
        public int TicketId { get; set; }
        public int DeliveryId { get; set; }
        public int ReliefId { get; set; }
        public int? HotelReservationId { get; set; }
        public int? TransportReservationId { get; set; }

        public Deliveries Delivery { get; set; }
        public HotelReservations HotelReservation { get; set; }
        public Reliefs Relief { get; set; }
        public Tickets Ticket { get; set; }
        public TransportReservations TransportReservation { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
