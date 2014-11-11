using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class aspnetuserclaimMap : EntityTypeConfiguration<aspnetuserclaim>
    {
        public aspnetuserclaimMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ClaimType)
                .HasMaxLength(256);

            this.Property(t => t.ClaimValue)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("aspnetuserclaims", "wvc");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasRequired(t => t.aspnetuser)
                .WithMany(t => t.aspnetuserclaims)
                .HasForeignKey(d => d.UserId);

        }
    }
}
