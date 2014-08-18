using System;
using System.Collections.Generic;

// namespace WVCApi.Models
namespace WVCApi.Models
{
    public partial class Village : BaseModel<Village>
    {
        public Village()
        {
            this.WoodVolumes = new List<WoodVolume>();
        }

		
				
		
        public string Name { get; set; }
		
				
				
				
		        public virtual ICollection<WoodVolume> WoodVolumes { get; set; }
    }
}
