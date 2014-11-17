using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_voucher_usersMap : EntityTypeConfiguration<wvc_voucher_users>
    {
        public wvc_voucher_usersMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .HasMaxLength(50);

            this.Property(t => t.sonof)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("wvc_voucher_users", "wvc");
            this.Property(t => t.id).HasColumnName("voucher_user_id");
            this.Property(t => t.voucher_id).HasColumnName("voucher_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.sonof).HasColumnName("sonof");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	        }
    }
}
