using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class villageMap : EntityTypeConfiguration<village>
    {
        public villageMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("village", "wvc");
            this.Property(t => t.id).HasColumnName("village_id");
            this.Property(t => t.name).HasColumnName("name");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	        }
    }
}
