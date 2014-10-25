using System;
using System.Collections.Generic;

namespace WVC.Models
{
    public partial class aspnetrole
    {
        public aspnetrole()
        {
            this.aspnetusers = new List<aspnetuser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<aspnetuser> aspnetusers { get; set; }
    }
}
