using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
            Users = new HashSet<ApplicationUser>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<Cities> Cities { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
