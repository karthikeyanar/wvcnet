using System;
using System.Collections.Generic;

namespace WVC.Models
{
    public partial class aspnetuser
    {
        public aspnetuser()
        {
            this.aspnetuserclaims = new List<aspnetuserclaim>();
            this.aspnetuserlogins = new List<aspnetuserlogin>();
            this.aspnetroles = new List<aspnetrole>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual ICollection<aspnetuserclaim> aspnetuserclaims { get; set; }
        public virtual ICollection<aspnetuserlogin> aspnetuserlogins { get; set; }
        public virtual ICollection<aspnetrole> aspnetroles { get; set; }
    }
}
