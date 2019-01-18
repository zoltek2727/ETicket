using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Tours
    {
        public Tours()
        {
            Events = new HashSet<Events>();
        }

        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public int PerformerId { get; set; }

        public Performers Performer { get; set; }
        public ICollection<Events> Events { get; set; }
    }
}
