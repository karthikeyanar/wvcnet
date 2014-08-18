using System.Data.Entity;
using System.Data.Entity.Infrastructure;
// using WVCApi.Models.Mapping;

// namespace WVCApi.Models
namespace WVCApi.Models
{
    //public partial class WVCContext : DbContext
	public partial class WVCApiContext : DbContext
    {
        //static WVCContext()
		static WVCApiContext()
        {
            //Database.SetInitializer<WVCContext>(null);
			Database.SetInitializer<WVCApiContext>(null);
        }

		//public WVCContext()
		public WVCApiContext()
			//: base("Name=WVCContext")
			: base("Name=WVCApiContext")
		{
		}

        public DbSet<District> Districts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Range> Ranges { get; set; }
        public DbSet<Taluk> Taluks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<WoodVolume> WoodVolumes { get; set; }
        public DbSet<WoodVolumeItem> WoodVolumeItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new DivisionMap());
            modelBuilder.Configurations.Add(new RangeMap());
            modelBuilder.Configurations.Add(new TalukMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new VillageMap());
            modelBuilder.Configurations.Add(new WoodVolumeMap());
            modelBuilder.Configurations.Add(new WoodVolumeItemMap());
        }
    }
}
