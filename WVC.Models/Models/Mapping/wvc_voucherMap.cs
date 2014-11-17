using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_voucherMap : EntityTypeConfiguration<wvc_voucher>
    {
        public wvc_voucherMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.sonof)
                .HasMaxLength(50);

            this.Property(t => t.address)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.place)
                .HasMaxLength(100);

            this.Property(t => t.description)
                .HasMaxLength(1000);

            this.Property(t => t.additional_description)
                .HasMaxLength(1000);

            this.Property(t => t.quantity)
                .HasPrecision(19,4);

            this.Property(t => t.quantity_type)
                .HasMaxLength(5);

            this.Property(t => t.rate)
                .HasPrecision(19,4);

            this.Property(t => t.per_type)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.amount)
                .HasPrecision(19,4);

            this.Property(t => t.wso_no_year)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.book_no)
                .HasMaxLength(20);

            this.Property(t => t.page_no)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("wvc_voucher", "wvc");
            this.Property(t => t.id).HasColumnName("voucher_id");
            this.Property(t => t.voucher_type_id).HasColumnName("voucher_type_id");
            this.Property(t => t.voucher_no).HasColumnName("voucher_no");
            this.Property(t => t.voucher_date).HasColumnName("voucher_date");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.sonof).HasColumnName("sonof");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.place).HasColumnName("place");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.additional_description).HasColumnName("additional_description");
            this.Property(t => t.quantity).HasColumnName("quantity");
            this.Property(t => t.quantity_type).HasColumnName("quantity_type");
            this.Property(t => t.rate).HasColumnName("rate");
            this.Property(t => t.per).HasColumnName("per");
            this.Property(t => t.per_type).HasColumnName("per_type");
            this.Property(t => t.amount).HasColumnName("amount");
            this.Property(t => t.wso_no).HasColumnName("wso_no");
            this.Property(t => t.wso_no_year).HasColumnName("wso_no_year");
            this.Property(t => t.account_type_id).HasColumnName("account_type_id");
            this.Property(t => t.district_id).HasColumnName("district_id");
            this.Property(t => t.range_id).HasColumnName("range_id");
            this.Property(t => t.agreement_date).HasColumnName("agreement_date");
            this.Property(t => t.book_no).HasColumnName("book_no");
            this.Property(t => t.page_no).HasColumnName("page_no");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	        }
    }
}
