using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class aspnetroleMap : EntityTypeConfiguration<aspnetrole>
    {
        public aspnetroleMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(166);

            // Table & Column Mappings
            this.ToTable("aspnetroles", "wvc");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasMany(t => t.aspnetusers)
                .WithMany(t => t.aspnetroles)
                .Map(m =>
                    {
                        m.ToTable("aspnetuserroles", "wvc");
                        m.MapLeftKey("RoleId");
                        m.MapRightKey("UserId");
                    });


        }
    }
}
