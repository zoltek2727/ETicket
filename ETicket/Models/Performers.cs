using System;
using System.Collections.Generic;

namespace ETicket.Models
{
    public partial class Performers
    {
        public Performers()
        {
            Tours = new HashSet<Tours>();
        }

        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public int PerformerCategoryId { get; set; }

        public PerformerCategories PerformerCategory { get; set; }
        public ICollection<Tours> Tours { get; set; }
    }
}
