using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace WVCApi.Models.Mapping
namespace WVCApi.Models
{
    public partial class RangeMap : EntityTypeConfiguration<Range>
    {
        public RangeMap()
        {
		            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Range");
            this.Property(t => t.ID).HasColumnName("RangeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDate).HasColumnName("LastUpdatedDate");
        }
    }
}
