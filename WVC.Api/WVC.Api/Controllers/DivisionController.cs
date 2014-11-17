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

	[RoutePrefix("Division")]
	public class DivisionController : BaseApiController<Division, wvc_division> {

		public DivisionController()
			: this(new DivisionRepository()) {
		}

		public DivisionController(IDivisionRepository currencyRepository) {
			_DivisionRepository = currencyRepository;
		}

		IDivisionRepository _DivisionRepository;

		[HttpGet]
		[ActionName("Select")]
		public List<AutoCompleteList> GetDivisions([FromUri] string term, [FromUri] int pageSize = 10) {
			return _DivisionRepository.GetDivisions(term, pageSize);
		}

		[Authorize(Roles = "member")]
		public override IHttpActionResult Post(Division contract) {
			return base.Post(contract);
		}

		[Authorize(Roles = "member")]
		public override IHttpActionResult Put(int id, Division contract) {
			return base.Put(id, contract);
		}

		[Authorize(Roles = "member")]
		public override IHttpActionResult Delete(int id) {
			return base.Delete(id);
		}
	}
	 
}