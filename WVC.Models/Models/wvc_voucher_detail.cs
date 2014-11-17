using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_voucher_detail : BaseEntity<wvc_voucher_detail>
    {
		
				
		
        public int voucher_id { get; set; }
		
		
        public string description { get; set; }
		
		
        public Nullable<decimal> detail_type_value { get; set; }
		
		
        public string detail_type { get; set; }
		
		
        public Nullable<decimal> detail_type_days { get; set; }
		
		
        public string detail_type_days_mode { get; set; }
		
		
        public Nullable<decimal> detail_type_calc { get; set; }
		
		
        public Nullable<decimal> detail_place_value { get; set; }
		
		
        public string detail_place_type { get; set; }
		
		
        public Nullable<decimal> detail_place_days { get; set; }
		
		
        public string detail_place_days_mode { get; set; }
		
		
        public Nullable<decimal> amount { get; set; }
        public virtual wvc_voucher wvc_voucher { get; set; }
    }
}
