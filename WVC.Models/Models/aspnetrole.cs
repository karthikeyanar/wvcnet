using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class aspnetrole : BaseEntity<aspnetrole>
    {
        public aspnetrole()
        {
            this.aspnetusers = new List<aspnetuser>();
        }

		
				
		
        public string Name { get; set; }
        public virtual ICollection<aspnetuser> aspnetusers { get; set; }
    }
}
