using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_voucher : BaseEntity<wvc_voucher>
    {
        public wvc_voucher()
        {
            this.wvc_voucher_detail = new List<wvc_voucher_detail>();
            this.wvc_voucher_inspection = new List<wvc_voucher_inspection>();
            this.wvc_voucher_period = new List<wvc_voucher_period>();
        }

		
				
		
        public int voucher_type_id { get; set; }
		
		
        public int voucher_no { get; set; }
		
		
        public System.DateTime voucher_date { get; set; }
		
		
        public string name { get; set; }
		
		
        public string sonof { get; set; }
		
		
        public string address { get; set; }
		
		
        public string place { get; set; }
		
		
        public string description { get; set; }
		
		
        public string additional_description { get; set; }
		
		
        public decimal quantity { get; set; }
		
		
        public string quantity_type { get; set; }
		
		
        public decimal rate { get; set; }
		
		
        public int per { get; set; }
		
		
        public string per_type { get; set; }
		
		
        public decimal amount { get; set; }
		
		
        public int wso_no { get; set; }
		
		
        public string wso_no_year { get; set; }
		
		
        public int account_type_id { get; set; }
		
		
        public Nullable<int> district_id { get; set; }
		
		
        public Nullable<int> range_id { get; set; }
		
		
        public Nullable<System.DateTime> agreement_date { get; set; }
		
		
        public string book_no { get; set; }
		
		
        public string page_no { get; set; }
        public virtual ICollection<wvc_voucher_detail> wvc_voucher_detail { get; set; }
        public virtual ICollection<wvc_voucher_inspection> wvc_voucher_inspection { get; set; }
        public virtual ICollection<wvc_voucher_period> wvc_voucher_period { get; set; }
    }
}
