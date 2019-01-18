using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Models
{
    public class Photos
    {
        public Photos()
        {
            PhotoEvents = new HashSet<PhotoEvents>();
        }

        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<PhotoEvents> PhotoEvents { get; set; }
    }
}
