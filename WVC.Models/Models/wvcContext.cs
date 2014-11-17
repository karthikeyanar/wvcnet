using System.Data.Entity;
using System.Data.Entity.Infrastructure;
// using CodeFirstClassGenerate.Models.Mapping;

// namespace CodeFirstClassGenerate.Models
namespace WVC.Models
{
    //public partial class wvcContext : DbContext
	public partial class WVCContext : DbContext
    {
        //static wvcContext()
		static WVCContext()
        {
            //Database.SetInitializer<wvcContext>(null);
			Database.SetInitializer<WVCContext>(null);
        }

		//public wvcContext()
		public WVCContext()
			//: base("Name=wvcContext")
			: base("Name=WVCContext")
		{
		}

        //public DbSet<aspnetrole> aspnetroles { get; set; }
        //public DbSet<aspnetuserclaim> aspnetuserclaims { get; set; }
        //public DbSet<aspnetuserlogin> aspnetuserlogins { get; set; }
        //public DbSet<aspnetuser> aspnetusers { get; set; }
        public DbSet<wvc_account_type> wvc_account_type { get; set; }
        public DbSet<wvc_district> wvc_district { get; set; }
        public DbSet<wvc_division> wvc_division { get; set; }
        public DbSet<wvc_range> wvc_range { get; set; }
        public DbSet<wvc_taluk> wvc_taluk { get; set; }
        public DbSet<wvc_user> wvc_user { get; set; }
        public DbSet<wvc_village> wvc_village { get; set; }
        public DbSet<wvc_voucher> wvc_voucher { get; set; }
        public DbSet<wvc_voucher_detail> wvc_voucher_detail { get; set; }
        public DbSet<wvc_voucher_inspection> wvc_voucher_inspection { get; set; }
        public DbSet<wvc_voucher_period> wvc_voucher_period { get; set; }
        public DbSet<wvc_voucher_type> wvc_voucher_type { get; set; }
        public DbSet<wvc_voucher_users> wvc_voucher_users { get; set; }
        public DbSet<wvc_wood_volum_item> wvc_wood_volum_item { get; set; }
        public DbSet<wvc_wood_volume> wvc_wood_volume { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new aspnetroleMap());
            //modelBuilder.Configurations.Add(new aspnetuserclaimMap());
            //modelBuilder.Configurations.Add(new aspnetuserloginMap());
            //modelBuilder.Configurations.Add(new aspnetuserMap());
            modelBuilder.Configurations.Add(new wvc_account_typeMap());
            modelBuilder.Configurations.Add(new wvc_districtMap());
            modelBuilder.Configurations.Add(new wvc_divisionMap());
            modelBuilder.Configurations.Add(new wvc_rangeMap());
            modelBuilder.Configurations.Add(new wvc_talukMap());
            modelBuilder.Configurations.Add(new wvc_userMap());
            modelBuilder.Configurations.Add(new wvc_villageMap());
            modelBuilder.Configurations.Add(new wvc_voucherMap());
            modelBuilder.Configurations.Add(new wvc_voucher_detailMap());
            modelBuilder.Configurations.Add(new wvc_voucher_inspectionMap());
            modelBuilder.Configurations.Add(new wvc_voucher_periodMap());
            modelBuilder.Configurations.Add(new wvc_voucher_typeMap());
            modelBuilder.Configurations.Add(new wvc_voucher_usersMap());
            modelBuilder.Configurations.Add(new wvc_wood_volum_itemMap());
            modelBuilder.Configurations.Add(new wvc_wood_volumeMap());
        }
    }
}
