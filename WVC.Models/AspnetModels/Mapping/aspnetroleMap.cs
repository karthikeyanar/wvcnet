using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WVC.Models
{
    public class aspnetroleMap : EntityTypeConfiguration<aspnetrole>
    {
        public aspnetroleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(166);

            // Table & Column Mappings
            this.ToTable("aspnetroles", "wvc");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
			//this.HasMany(t => t.aspnetusers)
			//	.WithMany(t => t.aspnetroles)
			//	.Map(m =>
			//		{
			//			m.ToTable("aspnetuserroles", "wvc");
			//			m.MapLeftKey("RoleId");
			//			m.MapRightKey("UserId");
			//		});


        }
    }
}
