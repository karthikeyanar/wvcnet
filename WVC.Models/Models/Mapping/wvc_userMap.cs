using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_userMap : EntityTypeConfiguration<wvc_user>
    {
        public wvc_userMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.aspnetuser_id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.first_name)
                .HasMaxLength(30);

            this.Property(t => t.last_name)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("wvc_user", "wvc");
            this.Property(t => t.id).HasColumnName("user_id");
            this.Property(t => t.aspnetuser_id).HasColumnName("aspnetuser_id");
            this.Property(t => t.first_name).HasColumnName("first_name");
            this.Property(t => t.last_name).HasColumnName("last_name");
            this.Property(t => t.is_active).HasColumnName("is_active");
            this.Property(t => t.created_by).HasColumnName("created_by");
            this.Property(t => t.created_date).HasColumnName("created_date");
            this.Property(t => t.last_updated_by).HasColumnName("last_updated_by");
            this.Property(t => t.last_updated_date).HasColumnName("last_updated_date");
        }
    }
}
