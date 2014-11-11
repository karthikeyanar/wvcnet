using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVC.Models {
    [MetadataType(typeof(wvc_wood_volume_itemMD))]
    public partial class wvc_wood_volum_item {

        public override void OnSaving() {
            base.OnSaving();
            if((this.co_efficient ?? 0) <= 0) {
                this.co_efficient = (decimal) 35.315;
            }
            try {
                this.volume = ((this.girth ?? 0) / 4) * ((this.girth ?? 0) / 4) * (this.length ?? 0);
                this.final_volume = (this.volume ?? 0) * (this.co_efficient ?? 0);
            } catch {
                this.volume = 0;
                this.final_volume = 0;
            }
        }

        public override bool Validate() {
            if(base.Validate()) {
                wvc_wood_volum_item item = Service.All().Where(x => x.id != this.id && x.description.Equals(this.description)).FirstOrDefault();
                if(item != null) {
                    this.Errors.Add("description","Description already exists");
                    return false;
                }
                return true;
            }
            return false;
        }

        public class wvc_wood_volume_itemMD {

            [Required(ErrorMessage = "Property is required")]
            [StringLength(200,ErrorMessage = "Property name must be under 200 characters.")]
            public string description { get; set; }

        }
    }
}
