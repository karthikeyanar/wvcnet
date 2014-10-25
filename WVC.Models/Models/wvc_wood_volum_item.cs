using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_wood_volum_item : BaseEntity<wvc_wood_volum_item>
    {
		
				
		
        public int wood_volume_id { get; set; }
		
		
        public string description { get; set; }
		
		
        public Nullable<float> length { get; set; }
		
		
        public Nullable<float> girth { get; set; }
		
		
        public Nullable<float> volume { get; set; }
		
		
        public Nullable<float> co_efficient { get; set; }
		
		
        public Nullable<float> final_volume { get; set; }
        public virtual wvc_wood_volume wvc_wood_volume { get; set; }
    }
}
