using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WVC.Models
{
    public class aspnetuserloginMap : EntityTypeConfiguration<aspnetuserlogin>
    {
        public aspnetuserloginMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });

            // Properties
            this.Property(t => t.LoginProvider)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ProviderKey)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("aspnetuserlogins", "wvc");
            this.Property(t => t.LoginProvider).HasColumnName("LoginProvider");
            this.Property(t => t.ProviderKey).HasColumnName("ProviderKey");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.aspnetuser)
                .WithMany(t => t.aspnetuserlogins)
                .HasForeignKey(d => d.UserId);

        }
    }
}
