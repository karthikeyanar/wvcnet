using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WVC.Views.Controllers {
	public class HomeController:Controller {
		public ActionResult Index() {
			return View();
		}

        public ActionResult Login() {
            return View();
        }

		public ActionResult WoodVolume() {
			return View();
		}

		public ActionResult AddWoodVolume() {
			return View();
		}
		 
	}
}