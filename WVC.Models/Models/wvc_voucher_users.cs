using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_voucher_users : BaseEntity<wvc_voucher_users>
    {
		
				
		
        public int voucher_id { get; set; }
		
		
        public string name { get; set; }
		
		
        public string sonof { get; set; }
    }
}
