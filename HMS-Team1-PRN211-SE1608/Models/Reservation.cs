using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationServices = new HashSet<ReservationService>();
        }

        public int AccountId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public double Price { get; set; }
        public DateTime EndDate { get; set; }
        public int ReservationId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Room Room { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual ICollection<ReservationService> ReservationServices { get; set; }
    }
}
