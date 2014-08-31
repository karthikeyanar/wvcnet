namespace WVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WoodVolume")]
    public partial class WoodVolume
    {
        public WoodVolume()
        {
            WoodVolumeItems = new HashSet<WoodVolumeItem>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Designation { get; set; }

        public int? DivisionId { get; set; }

        public int? RangeId { get; set; }

        public int? DistrictId { get; set; }

        public int? TalukId { get; set; }

        public int? VillageId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public virtual District District { get; set; }

        public virtual Division Division { get; set; }

        public virtual Range Range { get; set; }

        public virtual Taluk Taluk { get; set; }

        public virtual User User { get; set; }

        public virtual Village Village { get; set; }

        public virtual ICollection<WoodVolumeItem> WoodVolumeItems { get; set; }
    }
}
