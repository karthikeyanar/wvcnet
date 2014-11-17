using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_voucher_inspection : BaseEntity<wvc_voucher_inspection>
    {
		
				
		
        public int voucher_id { get; set; }
		
		
        public System.DateTime inspection_date { get; set; }
        public virtual wvc_voucher wvc_voucher { get; set; }
    }
}
