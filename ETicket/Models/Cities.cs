using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Hotels = new HashSet<Hotels>();
            Places = new HashSet<Places>();
            TransportReservations = new HashSet<TransportReservations>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public Countries Country { get; set; }
        public ICollection<Hotels> Hotels { get; set; }
        public ICollection<Places> Places { get; set; }
        public ICollection<TransportReservations> TransportReservations { get; set; }
    }
}
