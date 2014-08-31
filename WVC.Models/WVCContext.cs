namespace WVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class WVCContext : DbContext
    {
        public WVCContext()
            : base("name=WVCContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Range> Ranges { get; set; }
        public virtual DbSet<Taluk> Taluks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Village> Villages { get; set; }
        public virtual DbSet<WoodVolume> WoodVolumes { get; set; }
        public virtual DbSet<WoodVolumeItem> WoodVolumeItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<WoodVolumeItem>()
                .Property(e => e.Length)
                .HasPrecision(19, 8);

            modelBuilder.Entity<WoodVolumeItem>()
                .Property(e => e.Girth)
                .HasPrecision(19, 8);

            modelBuilder.Entity<WoodVolumeItem>()
                .Property(e => e.Volume)
                .HasPrecision(19, 8);

            modelBuilder.Entity<WoodVolumeItem>()
                .Property(e => e.CoEfficient)
                .HasPrecision(19, 8);

            modelBuilder.Entity<WoodVolumeItem>()
                .Property(e => e.FinalVolume)
                .HasPrecision(19, 8);
        }
    }
}
