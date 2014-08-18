using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace WVCApi.Models.Mapping
namespace WVCApi.Models
{
    public partial class DivisionMap : EntityTypeConfiguration<Division>
    {
        public DivisionMap()
        {
		            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Division");
            this.Property(t => t.ID).HasColumnName("DivisionID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDate).HasColumnName("LastUpdatedDate");
        }
    }
}
