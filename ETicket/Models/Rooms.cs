using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            HotelReservations = new HashSet<HotelReservations>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public int RoomBeds { get; set; }
        public decimal RoomPriceForNight { get; set; }
        public bool RoomAvailable { get; set; }
        public int HotelId { get; set; }

        public Hotels Hotel { get; set; }
        public ICollection<HotelReservations> HotelReservations { get; set; }
    }
}
