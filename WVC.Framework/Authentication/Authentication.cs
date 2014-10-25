using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace WVC.Framework {
	public static class Authentication {

		public const string IDKey = "id";
		public const string RoleKey = "role";

		public static int? CurrentUserID {
			get {
				var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
				int id = 0;
				if(claimsIdentity != null) {
					var claim = claimsIdentity.FindFirst(Authentication.IDKey);
					if(claim != null) {
						int.TryParse(claim.Value,out id);
					}
				}
				if(id > 0) { return id; } else { return null; }
			}
		}

		public static string CurrentRole {
			get {
				var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
				string role = string.Empty;
				if(claimsIdentity != null) {
					var claim = claimsIdentity.FindFirst(Authentication.RoleKey);
					if(claim != null) {
						role = claim.Value;
					}
				}
				return role;
			}
		}
		 
	}
}
