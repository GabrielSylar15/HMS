using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class ReservationService
    {
        public int ReservationId { get; set; }
        public int ServiceId { get; set; }
        public double Price { get; set; }
        public DateTime BookedDate { get; set; }
        public int Quantity { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Service Service { get; set; }
    }
}
