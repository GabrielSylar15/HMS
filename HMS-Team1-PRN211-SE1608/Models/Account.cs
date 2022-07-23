using System;
using System.Collections.Generic;

#nullable disable

namespace HMS_Team1_PRN211_SE1608.Models
{
    public partial class Account
    {
        public Account()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime DoB { get; set; }
        public int AccountId { get; set; }
        public string Phone { get; set; }
        public string DisplayName { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
