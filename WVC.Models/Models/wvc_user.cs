using System;
using System.Collections.Generic;
using WVC.Framework;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    public partial class wvc_user : BaseEntity<wvc_user>
    {
        public wvc_user()
        {
            this.wvc_wood_volume = new List<wvc_wood_volume>();
        }

		
				
		
        public string aspnetuser_id { get; set; }
		
		
        public string first_name { get; set; }
		
		
        public string last_name { get; set; }
		
		
        public Nullable<bool> is_active { get; set; }
		
				
				
				
		        public virtual ICollection<wvc_wood_volume> wvc_wood_volume { get; set; }
    }
}
