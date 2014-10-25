using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_wood_volume : BaseEntity<wvc_wood_volume>
    {
        public wvc_wood_volume()
        {
            this.wvc_wood_volum_item = new List<wvc_wood_volum_item>();
        }

		
				
		
        public Nullable<int> user_id { get; set; }
		
		
        public string name { get; set; }
		
		
        public string description { get; set; }
		
		
        public Nullable<int> division_id { get; set; }
		
		
        public Nullable<int> district_id { get; set; }
		
		
        public Nullable<int> range_id { get; set; }
		
		
        public Nullable<int> taluk_id { get; set; }
		
		
        public Nullable<int> village_id { get; set; }
        public virtual wvc_user wvc_user { get; set; }
        public virtual ICollection<wvc_wood_volum_item> wvc_wood_volum_item { get; set; }
    }
}
