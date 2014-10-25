using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WVC.Models
{
    public class aspnetuserclaimMap : EntityTypeConfiguration<aspnetuserclaim>
    {
        public aspnetuserclaimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

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
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");

            // Relationships
            this.HasRequired(t => t.aspnetuser)
                .WithMany(t => t.aspnetuserclaims)
                .HasForeignKey(d => d.UserId);

        }
    }
}
