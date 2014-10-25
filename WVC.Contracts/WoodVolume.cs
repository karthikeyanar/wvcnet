using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVC.Framework;

namespace WVC.Contracts {
	public class WoodVolume : BaseContract {
		
		public Nullable<int> user_id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public Nullable<int> division_id { get; set; }
		public string division_name { get; set; }
		public Nullable<int> district_id { get; set; }
		public string district_name { get; set; }
		public Nullable<int> range_id { get; set; }
		public string range_name { get; set; }
		public Nullable<int> taluk_id { get; set; }
		public string taluk_name { get; set; }
		public Nullable<int> village_id { get; set; }
		public string village_name { get; set; }
	}
}
