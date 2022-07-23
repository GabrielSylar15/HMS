using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class SizeRoom
    {
        public SizeRoom()
        {
            Rooms = new HashSet<Room>();
        }

        public int SizeId { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
