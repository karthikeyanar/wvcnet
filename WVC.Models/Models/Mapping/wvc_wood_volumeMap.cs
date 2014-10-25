using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_wood_volumeMap : EntityTypeConfiguration<wvc_wood_volume>
    {
        public wvc_wood_volumeMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("wvc_wood_volume", "wvc");
            this.Property(t => t.id).HasColumnName("wood_volume_id");
            this.Property(t => t.user_id).HasColumnName("user_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.division_id).HasColumnName("division_id");
            this.Property(t => t.district_id).HasColumnName("district_id");
            this.Property(t => t.range_id).HasColumnName("range_id");
            this.Property(t => t.taluk_id).HasColumnName("taluk_id");
            this.Property(t => t.village_id).HasColumnName("village_id");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasOptional(t => t.wvc_user)
                .WithMany(t => t.wvc_wood_volume)
                .HasForeignKey(d => d.user_id);

        }
    }
}
