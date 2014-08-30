using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Web.Http;

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

		public JsonResult GetDivisions([FromUri] Paging paging) {
			List<Division> rows = new List<Division>();
			int rowsLength = 5;
			for (int i = 0; i <= rowsLength; i++) {
				rows.Add(new Division { ID = (i + 1), Name = "Name " + (i + 1) });
			}
			return Json(new PaginatedListResult { total = 10, rows = rows }, JsonRequestBehavior.AllowGet);
		}

	}

	public class Division {
		public int ID { get; set; }
		public string Name { get; set; }
	}

	public class Paging {

		public Paging() {
			this.PageSize = 10;
			this.PageIndex = 1;
			this.SortName = "ID";
			this.SortOrder = "asc";
			this.Total = 0;
		}

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public string SortName { get; set; }

		public string SortOrder { get; set; }

		public int Total { get; set; }
	}

	public class PaginatedListResult {
		public int total;
		public IEnumerable<object> rows;
	}

}
