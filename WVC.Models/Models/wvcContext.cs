using System.Data.Entity;
using System.Data.Entity.Infrastructure;
// using CodeFirstClassGenerate.Models.Mapping;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    //public partial class WVCContext : DbContext
	public partial class WVCContext : DbContext
    {
        //static WVCContext()
		static WVCContext()
        {
            //Database.SetInitializer<WVCContext>(null);
			Database.SetInitializer<WVCContext>(null);
        }

		//public WVCContext()
		public WVCContext()
			//: base("Name=WVCContext")
			: base("Name=WVCContext")
		{
		}

        //public DbSet<aspnetrole> aspnetroles { get; set; }
        //public DbSet<aspnetuserclaim> aspnetuserclaims { get; set; }
        //public DbSet<aspnetuserlogin> aspnetuserlogins { get; set; }
        //public DbSet<aspnetuser> aspnetusers { get; set; }
        public DbSet<district> districts { get; set; }
        public DbSet<division> divisions { get; set; }
        public DbSet<range> ranges { get; set; }
        public DbSet<taluk> taluks { get; set; }
        public DbSet<village> villages { get; set; }
        public DbSet<wvc_user> wvc_user { get; set; }
        public DbSet<wvc_wood_volum_item> wvc_wood_volum_item { get; set; }
        public DbSet<wvc_wood_volume> wvc_wood_volume { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new aspnetroleMap());
            //modelBuilder.Configurations.Add(new aspnetuserclaimMap());
            //modelBuilder.Configurations.Add(new aspnetuserloginMap());
            //modelBuilder.Configurations.Add(new aspnetuserMap());
            modelBuilder.Configurations.Add(new districtMap());
            modelBuilder.Configurations.Add(new divisionMap());
            modelBuilder.Configurations.Add(new rangeMap());
            modelBuilder.Configurations.Add(new talukMap());
            modelBuilder.Configurations.Add(new villageMap());
            modelBuilder.Configurations.Add(new wvc_userMap());
            modelBuilder.Configurations.Add(new wvc_wood_volum_itemMap());
            modelBuilder.Configurations.Add(new wvc_wood_volumeMap());
        }
    }
}
