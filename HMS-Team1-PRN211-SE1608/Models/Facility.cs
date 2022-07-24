using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Facility
    {
        public Facility()
        {
            FacilityRooms = new HashSet<FacilityRoom>();
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; }

        public virtual ICollection<FacilityRoom> FacilityRooms { get; set; }
    }
}
