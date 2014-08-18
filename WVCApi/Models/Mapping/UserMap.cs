using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

// namespace WVCApi.Models.Mapping
namespace WVCApi.Models
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
		            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PasswordSalt)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MiddleName)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("UserID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsAdmin).HasColumnName("IsAdmin");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.LastUpdatedBy).HasColumnName("LastUpdatedBy");
            this.Property(t => t.LastUpdatedDate).HasColumnName("LastUpdatedDate");
        }
    }
}
