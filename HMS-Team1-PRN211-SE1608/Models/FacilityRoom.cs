using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class FacilityRoom
    {
        public int FacilityId { get; set; }
        public int RoomId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual Room Room { get; set; }
    }
}
