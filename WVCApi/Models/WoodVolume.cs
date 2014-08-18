using System;
using System.Collections.Generic;

// namespace WVCApi.Models
namespace WVCApi.Models
{
    public partial class WoodVolume : BaseModel<WoodVolume>
    {
        public WoodVolume()
        {
            this.WoodVolumeItems = new List<WoodVolumeItem>();
        }

		
				
		
        public int UserID { get; set; }
		
		
        public string Designation { get; set; }
		
		
        public Nullable<int> DivisionID { get; set; }
		
		
        public Nullable<int> RangeID { get; set; }
		
		
        public Nullable<int> DistrictID { get; set; }
		
		
        public Nullable<int> TalukID { get; set; }
		
		
        public Nullable<int> VillageID { get; set; }
		
				
				
				
		        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
        public virtual Range Range { get; set; }
        public virtual Taluk Taluk { get; set; }
        public virtual User User { get; set; }
        public virtual Village Village { get; set; }
        public virtual ICollection<WoodVolumeItem> WoodVolumeItems { get; set; }
    }
}
