using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WVC.Framework;
using WVC.Api.Models;
using WVC.Models;

namespace WVC.Api.Providers {
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider {
		private readonly string _publicClientId;
		private readonly Func<UserManager<IdentityUser>> _userManagerFactory;

		public ApplicationOAuthProvider(string publicClientId, Func<UserManager<IdentityUser>> userManagerFactory) {
			if (publicClientId == null) {
				throw new ArgumentNullException("publicClientId");
			}

			if (userManagerFactory == null) {
				throw new ArgumentNullException("userManagerFactory");
			}

			_publicClientId = publicClientId;
			_userManagerFactory = userManagerFactory;
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
			using (UserManager<IdentityUser> userManager = _userManagerFactory()) {
				IdentityUser user = await userManager.FindAsync(context.UserName, context.Password);
				WVCUserManager wvcUserManager = new WVCUserManager();
				IdentityManager identityManager = new IdentityManager();
				wvc_user wvcUser = null;
				IdentityUserRole userRole = null;
				IdentityRole role = null;

				if (user == null) {
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				} else {
					userRole = user.Roles.FirstOrDefault();
					if (userRole == null) {
						context.SetError("invalid_grant", "The user is inactive (no rules assigned). Contact administrator.");
						return;
					}
					role = identityManager.GetRoleById(userRole.RoleId);
					// check wvc user active;
					wvcUser = wvcUserManager.FindUser(user.Id);
					if (wvcUser == null) {
						context.SetError("invalid_grant", "The user is inactive. Contact administrator.");
						return;
					}
				}

				// Add claims
				ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
				oAuthIdentity.AddClaim(new Claim(Authentication.IDKey, wvcUser.id.ToString()));
				oAuthIdentity.AddClaim(new Claim(Authentication.RoleKey, role.Name));

				ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
				AuthenticationProperties properties = CreateProperties(user, role, wvcUser);
				AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
				context.Validated(ticket);
				context.Request.Context.Authentication.SignIn(cookiesIdentity);
			}
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context) {
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary) {
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
			// Resource owner password credentials does not provide a client ID.
			if (context.ClientId == null) {
				context.Validated();
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
			if (context.ClientId == _publicClientId) {
				Uri expectedRootUri = new Uri(context.Request.Uri, "/");

				if (expectedRootUri.AbsoluteUri == context.RedirectUri) {
					context.Validated();
				}
			}

			return Task.FromResult<object>(null);
		}

		public static AuthenticationProperties CreateProperties(string userName) {
			IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
			return new AuthenticationProperties(data);
		}

		public static AuthenticationProperties CreateProperties(IdentityUser user, IdentityRole role, wvc_user wvcUser) {
			IDictionary<string, string> data = new Dictionary<string, string>
			{
				{ "user_name", user.UserName },
				{ "role", role.Name},
				{ "id", wvcUser.id.ToString()},
				{ "first_name", (wvcUser.first_name != null ? wvcUser.first_name : "")},
				{ "last_name", (wvcUser.last_name != null ? wvcUser.last_name : "")}
			};
			return new AuthenticationProperties(data);
		}
	}
}