namespace WVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WoodVolumeItem")]
    public partial class WoodVolumeItem
    {
        public int Id { get; set; }

        public int WoodVolumeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Property { get; set; }

        public decimal? Length { get; set; }

        public decimal? Girth { get; set; }

        public decimal? Volume { get; set; }

        public decimal? CoEfficient { get; set; }

        public decimal? FinalVolume { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public virtual WoodVolume WoodVolume { get; set; }
    }
}
