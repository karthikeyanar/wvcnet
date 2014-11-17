using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_voucher_detailMap : EntityTypeConfiguration<wvc_voucher_detail>
    {
        public wvc_voucher_detailMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.detail_type_value)
                .HasPrecision(19,4);

            this.Property(t => t.detail_type)
                .HasMaxLength(5);

            this.Property(t => t.detail_type_days)
                .HasPrecision(19,4);

            this.Property(t => t.detail_type_days_mode)
                .HasMaxLength(5);

            this.Property(t => t.detail_type_calc)
                .HasPrecision(19,4);

            this.Property(t => t.detail_place_value)
                .HasPrecision(19,4);

            this.Property(t => t.detail_place_type)
                .HasMaxLength(5);

            this.Property(t => t.detail_place_days)
                .HasPrecision(19,4);

            this.Property(t => t.detail_place_days_mode)
                .HasMaxLength(5);

            this.Property(t => t.amount)
                .HasPrecision(19,4);

            // Table & Column Mappings
            this.ToTable("wvc_voucher_detail", "wvc");
            this.Property(t => t.id).HasColumnName("voucher_detail_id");
            this.Property(t => t.voucher_id).HasColumnName("voucher_id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.detail_type_value).HasColumnName("detail_type_value");
            this.Property(t => t.detail_type).HasColumnName("detail_type");
            this.Property(t => t.detail_type_days).HasColumnName("detail_type_days");
            this.Property(t => t.detail_type_days_mode).HasColumnName("detail_type_days_mode");
            this.Property(t => t.detail_type_calc).HasColumnName("detail_type_calc");
            this.Property(t => t.detail_place_value).HasColumnName("detail_place_value");
            this.Property(t => t.detail_place_type).HasColumnName("detail_place_type");
            this.Property(t => t.detail_place_days).HasColumnName("detail_place_days");
            this.Property(t => t.detail_place_days_mode).HasColumnName("detail_place_days_mode");
            this.Property(t => t.amount).HasColumnName("amount");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasRequired(t => t.wvc_voucher)
                .WithMany(t => t.wvc_voucher_detail)
                .HasForeignKey(d => d.voucher_id);

        }
    }
}
