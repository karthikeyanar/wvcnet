using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class aspnetuserlogin : BaseEntity<aspnetuserlogin>
    {
		
		
        public string LoginProvider { get; set; }
		
		
        public string ProviderKey { get; set; }
		
		
        public string UserId { get; set; }
        public virtual aspnetuser aspnetuser { get; set; }
    }
}
