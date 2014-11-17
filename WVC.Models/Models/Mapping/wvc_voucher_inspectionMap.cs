using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_voucher_inspectionMap : EntityTypeConfiguration<wvc_voucher_inspection>
    {
        public wvc_voucher_inspectionMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("wvc_voucher_inspection", "wvc");
            this.Property(t => t.id).HasColumnName("voucher_inspection_id");
            this.Property(t => t.voucher_id).HasColumnName("voucher_id");
            this.Property(t => t.inspection_date).HasColumnName("inspection_date");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasRequired(t => t.wvc_voucher)
                .WithMany(t => t.wvc_voucher_inspection)
                .HasForeignKey(d => d.voucher_id);

        }
    }
}
