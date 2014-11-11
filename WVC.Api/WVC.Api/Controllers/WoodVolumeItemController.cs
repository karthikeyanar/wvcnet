using WVC.Api.Repository;
using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WVC.Models;
using WVC.Contracts;

namespace WVC.Api.Controllers {

    [Authorize(Roles = "member")]
    [RoutePrefix("WoodVolumeItem")]
    public class WoodVolumeItemController:BaseApiController<WoodVolumeItem,wvc_wood_volum_item> {
        public WoodVolumeItemController() {
        }
    }
}