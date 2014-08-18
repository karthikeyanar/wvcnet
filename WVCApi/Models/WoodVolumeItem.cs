using System;
using System.Collections.Generic;

// namespace WVCApi.Models
namespace WVCApi.Models
{
    public partial class WoodVolumeItem : BaseModel<WoodVolumeItem>
    {
		
				
		
        public int WoodVolumeID { get; set; }
		
		
        public string Property { get; set; }
		
		
        public decimal Length { get; set; }
		
		
        public decimal Girth { get; set; }
		
		
        public decimal Volume { get; set; }
		
		
        public decimal CoEfficient { get; set; }
		
		
        public decimal FinalVolume { get; set; }
		
				
				
				
		        public virtual WoodVolume WoodVolume { get; set; }
    }
}
