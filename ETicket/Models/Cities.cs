using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Places = new HashSet<Places>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public Countries Country { get; set; }
        public ICollection<Places> Places { get; set; }
    }
}
