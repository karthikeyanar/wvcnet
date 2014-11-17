using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_voucher_period : BaseEntity<wvc_voucher_period>
    {
		
				
		
        public Nullable<int> voucher_id { get; set; }
		
		
        public Nullable<System.DateTime> start_date { get; set; }
		
		
        public Nullable<System.DateTime> end_date { get; set; }
        public virtual wvc_voucher wvc_voucher { get; set; }
    }
}
