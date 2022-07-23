using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string Note { get; set; }
        public int Rated { get; set; }
        public DateTime FeedbackDate { get; set; }
        public int ReservationId { get; set; }
        public string ImageName { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
