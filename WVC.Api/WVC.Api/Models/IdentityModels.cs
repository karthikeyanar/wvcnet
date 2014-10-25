using WVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WVC.Api.Models {
	// You can add profile data for the user by adding more properties to your ApplicationUser
	// class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser:IdentityUser {
	}

	public class ApplicationDbContext:IdentityDbContext<ApplicationUser> {
		static ApplicationDbContext() {
			Database.SetInitializer(new MySqlInitializer());
		}

		public ApplicationDbContext()
			: base("DefaultConnection") {
		}
	}

	public class IdentityManager {
		public bool RoleExists(string name) {
			var rm = new RoleManager<IdentityRole>(
				new RoleStore<IdentityRole>(new ApplicationDbContext()));
			return rm.RoleExists(name);
		}

		public IdentityRole GetRoleById(string roleId) {
			var rm = new RoleManager<IdentityRole>(
				new RoleStore<IdentityRole>(new ApplicationDbContext()));
			return rm.FindById(roleId);
		}

		public bool CreateRole(string name) {
			if(this.RoleExists(name))
				return true;
			var rm = new RoleManager<IdentityRole>(
				new RoleStore<IdentityRole>(new ApplicationDbContext()));
			var idResult = rm.Create(new IdentityRole(name));
			return idResult.Succeeded;
		}

		public ApplicationUser GetUser(string userName) {
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
			return um.FindByName(userName);
		}

		public ApplicationUser GetUserById(string id) {
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
			return um.FindById(id);
		}

		public bool CreateUser(string userName,string password,out string errorResult) {
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
			var user = new ApplicationUser { UserName = userName, Email = userName };
			var idResult = um.Create(user,password);
			errorResult = GetErrorResult(idResult);
			return idResult.Succeeded;
		}

		public bool DeleteUser(string userName, out string errorResult) {
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
			var user = this.GetUser(userName);
			if (user != null) {
				var idResult = um.Delete(user);
				errorResult = GetErrorResult(idResult);
				return idResult.Succeeded;
			} else {
				errorResult = "User does not exist";
				return false;
			}
		}

		public bool ChangePassword(string userName, string password, out string errorResult) {
			var user = this.GetUser(userName);
			if (user != null) {
				var um = new UserManager<ApplicationUser>(
						new UserStore<ApplicationUser>(new ApplicationDbContext()));
				var idResult = um.RemovePassword(user.Id);
				if (idResult.Succeeded) { 
					 idResult = um.AddPassword(user.Id, password);
					 errorResult = GetErrorResult(idResult);
					 return idResult.Succeeded;
				} else {
					errorResult = GetErrorResult(idResult);
					return idResult.Succeeded;
				}
			} else {
				errorResult = "User does not exist";
				return false;
			}
		}

		private string GetErrorResult(IdentityResult result) {
			if(result == null) {
				return string.Empty;
			}

			string errorResult = string.Empty;
			if(!result.Succeeded) {
				if(result.Errors != null) {
					foreach(string error in result.Errors) {
						//ModelState.AddModelError("",error);
						errorResult += error;
					}
				}
			}

			return errorResult;
		}

		public bool AddUserToRole(string userId,string roleName) {
			var um = new UserManager<ApplicationUser>(
				new UserStore<ApplicationUser>(new ApplicationDbContext()));
			var idResult = um.AddToRole(userId,roleName);
			return idResult.Succeeded;
		}

		public List<IdentityRole> GetRoles(string userName) {
			var um = this.GetUser(userName);
			var currentRoles=new List<IdentityUserRole>();
			List<IdentityRole> aspRoles=new List<IdentityRole>();
			if (um != null) {
				currentRoles.AddRange(um.Roles);
				foreach (var role in currentRoles) {
					var findRole = this.GetRoleById(role.RoleId);
					if (findRole != null)
						aspRoles.Add(findRole);
				}
			}
			return aspRoles;
		}

		public bool CheckAnyOtherRoleByUser(string userName, string roleName, out string error) {
			List<IdentityRole> roles = this.GetRoles(userName);
			string exitRole = string.Empty;
			bool isResult = false;
			error = string.Empty;
			foreach (var role in roles) {
				if (role.Name != roleName) {
					exitRole = role.Name;
				}
			}
			if (exitRole != "") {
				string existRoleName = string.Empty;
				switch (exitRole) {
					case "EA": existRoleName = "WVC Admin"; break;
					case "EM": existRoleName = "WVC Member"; break;
					case "CA": existRoleName = "Company Admin"; break;
					case "CM": existRoleName = "Company Member"; break;
					case "GA": existRoleName = "Group Admin"; break;
					case "GM": existRoleName = "Group Member"; break;
					case "AA": existRoleName = "Agent Admin"; break;
					case "AM": existRoleName = "Agent Member"; break;
				}
				if (existRoleName != "") {
					error = existRoleName + " Role already added this email " + userName;
				}
			} else {
				isResult = true;
			}
			return isResult;
		}

		//public void ClearUserRoles(string userId) {
		//	var um = new UserManager<ApplicationUser>(
		//		new UserStore<ApplicationUser>(new ApplicationDbContext()));
		//	var user = um.FindById(userId);
		//	var currentRoles=new List<IdentityUserRole>();
		//	currentRoles.AddRange(user.Roles);
		//	foreach(var role in currentRoles) {
		//		var findRole = this.GetRoleById(role.RoleId);
		//		if(findRole != null)
		//			um.RemoveFromRole(userId,findRole.Name);
		//	}
		//}


	}

	public class WVCUserManager {

		public wvc_user FindUser(string aspnetUserId) {
			using (WVCContext context = new WVCContext()) {
				return context.wvc_user.FirstOrDefault(q => q.aspnetuser_id == aspnetUserId && q.is_active == true);
			}
		}

	}
}