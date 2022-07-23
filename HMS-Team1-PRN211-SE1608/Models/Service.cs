using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Service
    {
        public Service()
        {
            ReservationServices = new HashSet<ReservationService>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string ỊmageName { get; set; }

        public virtual ICollection<ReservationService> ReservationServices { get; set; }
    }
}
