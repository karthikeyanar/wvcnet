using WVC.Api.Models;
using WVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace WVC.Api.Controllers {

    // [Authorize]
    public class InitController:ApiController {

        // GET api/values
        [ActionName("Setup")]
        [HttpGet]
        public IHttpActionResult Setup() {
            IdentityManager identity = new IdentityManager();
            identity.CreateRole("admin");
            identity.CreateRole("member");
            CreateMember("admin@wvc.com","123456","admin","admin");
            CreateMember("member@wvc.com","123456","member","member");
            return Ok("Success");
        }

        private void CreateMember(string email,string password,string role,string firstName) {
            IdentityManager identity = new IdentityManager();
            var adminUser = identity.GetUser(email);
            if(adminUser == null) {
                string errorResult = string.Empty;
                if(identity.CreateUser(email,password,out errorResult)) {
                    adminUser = identity.GetUser(email);
                    if(adminUser != null) {
                        identity.AddUserToRole(adminUser.Id,role);
                        using(WVCContext context = new WVCContext()) {
                            var wvcUser = context.wvc_user.FirstOrDefault(q => q.aspnetuser_id == adminUser.Id);
                            if(wvcUser == null) {
                                wvcUser = new wvc_user();
                                wvcUser.aspnetuser_id = adminUser.Id;
                                wvcUser.created_date = DateTime.Now;
                                wvcUser.first_name = firstName;
                                wvcUser.is_active = true;
                                wvcUser.Save();
                            }
                        }
                    }
                }
            }
        }
    }
}
