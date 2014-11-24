using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVC.Framework;

namespace WVC.Contracts {
    public class Voucher:BaseContract {

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
	}

    public class VoucherDetail:BaseContract {
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
    }

    public class VoucherInspection:BaseContract {
        public int voucher_id { get; set; }
        public System.DateTime inspection_date { get; set; }
    }

    public class VoucherPeriod:BaseContract {
        public Nullable<int> voucher_id { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
    }

    public class VoucherUsers:BaseContract {
        public int voucher_id { get; set; }
        public string name { get; set; }
        public string sonof { get; set; }
    }


}
