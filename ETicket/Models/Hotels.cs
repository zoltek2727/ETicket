using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Hotels
    {
        public Hotels()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public string HotelAddress { get; set; }
        public string HotelPhoneNumber { get; set; }
        public int HotelRoomsNumber { get; set; }
        public int CityId { get; set; }

        public Cities City { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
    }
}
