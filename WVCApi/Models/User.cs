using System;
using System.Collections.Generic;

// namespace WVCApi.Models
namespace WVCApi.Models
{
    public partial class User : BaseModel<User>
    {
        public User()
        {
            this.WoodVolumes = new List<WoodVolume>();
        }

		
				
		
        public string Email { get; set; }
		
		
        public string PasswordHash { get; set; }
		
		
        public string PasswordSalt { get; set; }
		
		
        public string FirstName { get; set; }
		
		
        public string LastName { get; set; }
		
		
        public string MiddleName { get; set; }
		
		
        public bool IsActive { get; set; }
		
		
        public bool IsAdmin { get; set; }
		
		
        public string Mobile { get; set; }
		
				
				
				
		        public virtual ICollection<WoodVolume> WoodVolumes { get; set; }
    }
}
