using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace WVCApi.Models.Mapping
namespace WVCApi.Models
{
    public partial class WoodVolumeItemMap : EntityTypeConfiguration<WoodVolumeItem>
    {
        public WoodVolumeItemMap()
        {
		            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Property)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Length)
                .HasPrecision(19,4);

            this.Property(t => t.Girth)
                .HasPrecision(19,4);

            this.Property(t => t.Volume)
                .HasPrecision(19,4);

            this.Property(t => t.CoEfficient)
                .HasPrecision(19,4);

            this.Property(t => t.FinalVolume)
                .HasPrecision(19,4);

            // Table & Column Mappings
            this.ToTable("WoodVolumeItem");
            this.Property(t => t.ID).HasColumnName("WoodVolumeItemID");
            this.Property(t => t.WoodVolumeID).HasColumnName("WoodVolumeID");
            this.Property(t => t.Property).HasColumnName("Property");
            this.Property(t => t.Length).HasColumnName("Length");
            this.Property(t => t.Girth).HasColumnName("Girth");
            this.Property(t => t.Volume).HasColumnName("Volume");
            this.Property(t => t.CoEfficient).HasColumnName("CoEfficient");
            this.Property(t => t.FinalVolume).HasColumnName("FinalVolume");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDate).HasColumnName("LastUpdatedDate");

            // Relationships
            this.HasRequired(t => t.WoodVolume)
                .WithMany(t => t.WoodVolumeItems)
                .HasForeignKey(d => d.WoodVolumeID);

        }
    }
}
