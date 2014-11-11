using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace CodeFirstClassGenerate.Models.Mapping
namespace WVC.Models
{
    public partial class wvc_wood_volum_itemMap : EntityTypeConfiguration<wvc_wood_volum_item>
    {
        public wvc_wood_volum_itemMap()
        {
		            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.length)
                .HasPrecision(19,4);

            this.Property(t => t.girth)
                .HasPrecision(19,4);

            this.Property(t => t.volume)
                .HasPrecision(19,4);

            this.Property(t => t.co_efficient)
                .HasPrecision(19,4);

            this.Property(t => t.final_volume)
                .HasPrecision(19,4);

            // Table & Column Mappings
            this.ToTable("wvc_wood_volum_item", "wvc");
            this.Property(t => t.id).HasColumnName("wood_volume_item_id");
            this.Property(t => t.wood_volume_id).HasColumnName("wood_volume_id");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.length).HasColumnName("length");
            this.Property(t => t.girth).HasColumnName("girth");
            this.Property(t => t.volume).HasColumnName("volume");
            this.Property(t => t.co_efficient).HasColumnName("co_efficient");
            this.Property(t => t.final_volume).HasColumnName("final_volume");
	       Ignore(t=>t.created_date);
		       Ignore(t=>t.created_by);
		       Ignore(t=>t.last_updated_date);
		       Ignore(t=>t.last_updated_by);
	
            // Relationships
            this.HasRequired(t => t.wvc_wood_volume)
                .WithMany(t => t.wvc_wood_volum_item)
                .HasForeignKey(d => d.wood_volume_id);

        }
    }
}
