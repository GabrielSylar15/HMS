using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Room
    {
        public Room()
        {
            FacilityRooms = new HashSet<FacilityRoom>();
            Reservations = new HashSet<Reservation>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int SizeId { get; set; }
        public string RoomDescription { get; set; }
        public string RoomImage { get; set; }

        public virtual SizeRoom Size { get; set; }
        public virtual ICollection<FacilityRoom> FacilityRooms { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
