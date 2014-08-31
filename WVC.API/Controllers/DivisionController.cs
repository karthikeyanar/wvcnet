using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WVC.API.Controllers
{
    [Authorize]
    public class DivisionController : Controller
    {
        public ActionResult Index()
        {
            var userIdentity = User.Identity;
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
