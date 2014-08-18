using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace WVCApi.Models.Mapping
namespace WVCApi.Models
{
    public partial class WoodVolumeMap : EntityTypeConfiguration<WoodVolume>
    {
        public WoodVolumeMap()
        {
		            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Designation)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("WoodVolume");
            this.Property(t => t.ID).HasColumnName("WoodVolumeID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Designation).HasColumnName("Designation");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.RangeID).HasColumnName("RangeID");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.TalukID).HasColumnName("TalukID");
            this.Property(t => t.VillageID).HasColumnName("VillageID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDate).HasColumnName("LastUpdatedDate");

            // Relationships
            this.HasOptional(t => t.District)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.DistrictID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.DivisionID);
            this.HasOptional(t => t.Range)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.RangeID);
            this.HasOptional(t => t.Taluk)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.TalukID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.UserID);
            this.HasOptional(t => t.Village)
                .WithMany(t => t.WoodVolumes)
                .HasForeignKey(d => d.VillageID);

        }
    }
}
