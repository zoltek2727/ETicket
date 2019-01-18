using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Sectors
    {
        public Sectors()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int SectorId { get; set; }
        public string SectorName { get; set; }
        public int SectorAvailability { get; set; }
        public int? SectorCapacity { get; set; }
        public int? SectorRows { get; set; }
        public int? SectorRowPlaces { get; set; }
        public int? PlaceId { get; set; }

        public Places Place { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
