using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace WVCView.Controllers {
	public class HomeController : Controller {
		//
		// GET: /Home/

		public ActionResult Index() {
			return View();
		}

		public ActionResult Dashboard() {
			return View();
		}

		public ActionResult AboutUs() {
			Thread.Sleep(5000);
			return View();
		}

		public ActionResult Contact() {
			return View();
		}

		public ActionResult Division() {
			return View();
		}
	}
}
