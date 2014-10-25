using System;
using System.Collections.Generic;

namespace WVC.Models
{
    public partial class aspnetuserclaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public virtual aspnetuser aspnetuser { get; set; }
    }
}
