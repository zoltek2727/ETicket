using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class PerformerCategories
    {
        public PerformerCategories()
        {
            Performers = new HashSet<Performers>();
        }

        public int PerformerCategoryId { get; set; }
        public string PerformerCategoryName { get; set; }

        public ICollection<Performers> Performers { get; set; }
    }
}
