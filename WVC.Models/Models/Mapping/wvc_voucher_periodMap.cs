using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_voucher_periodMap : EntityTypeConfiguration<wvc_voucher_period>
    {
        public wvc_voucher_periodMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("wvc_voucher_period", "wvc");
            this.Property(t => t.id).HasColumnName("voucher_period_id");
            this.Property(t => t.voucher_id).HasColumnName("voucher_id");
            this.Property(t => t.start_date).HasColumnName("start_date");
            this.Property(t => t.end_date).HasColumnName("end_date");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasOptional(t => t.wvc_voucher)
                .WithMany(t => t.wvc_voucher_period)
                .HasForeignKey(d => d.voucher_id);

        }
    }
}
