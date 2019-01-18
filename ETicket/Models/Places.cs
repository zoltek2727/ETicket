using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Places
    {
        public Places()
        {
            Events = new HashSet<Events>();
            Sectors = new HashSet<Sectors>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public int PlaceCapacity { get; set; }
        public string PlaceAddress { get; set; }
        public int CityId { get; set; }

        public Cities City { get; set; }
        public ICollection<Events> Events { get; set; }
        public ICollection<Sectors> Sectors { get; set; }
    }
}
