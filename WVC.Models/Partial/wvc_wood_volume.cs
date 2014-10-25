using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVC.Models {
	[MetadataType(typeof(wvc_wood_volumeMD))]
	public partial class wvc_wood_volume {

		//public override void OnDeleting() {
		//	using (EcamContext context = new EcamContext()) {
		//		var country = context.wvc_wood_volume.Where(q => q.id == this.id).FirstOrDefault();
		//		if (country != null) {
		//			if (country.ec_airport.Count() > 0) {
		//				this.Errors.Errors.Add(new ErrorInfo { ErrorMessage = "Cann't Delete! Child record found!" })
		//			}
		//		}
		//	}
		//	base.OnDeleting();
		//}

		public override void OnSaving() {
			base.OnSaving();
			if ((this.division_id ?? 0) <= 0) {
				this.division_id = null;
			}
			if ((this.district_id ?? 0) <= 0) {
				this.district_id = null;
			}
			if ((this.range_id ?? 0) <= 0) {
				this.range_id = null;
			}
			if ((this.village_id ?? 0) <= 0) {
				this.village_id = null;
			}
			if ((this.taluk_id ?? 0) <= 0) {
				this.taluk_id = null;
			}
		}

		public override bool Validate() {
			if (base.Validate()) {
				wvc_wood_volume wvcWoodVolume = Service.All().Where(x => x.id != this.id && x.name.Equals(this.name)).FirstOrDefault();
				if (wvcWoodVolume != null) {
					this.Errors.Add("name", "Volume name already exists");
					return false;
				}
				return true;
			}
			return false;
		}

		public class wvc_wood_volumeMD {

			[Required(ErrorMessage = "Volume name is required")]
			[StringLength(50, ErrorMessage = "Volume name must be under 50 characters.")]
			public string name { get; set; }

			[StringLength(200, ErrorMessage = "Description must be under 200 characters.")]
			public string description { get; set; }

		}
	}
}
