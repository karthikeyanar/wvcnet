using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class aspnetuserclaim : BaseEntity<aspnetuserclaim>
    {
		
				
		
        public string UserId { get; set; }
		
		
        public string ClaimType { get; set; }
		
		
        public string ClaimValue { get; set; }
        public virtual aspnetuser aspnetuser { get; set; }
    }
}
