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

	[RoutePrefix("Taluk")]
	public class TalukController : BaseApiController<Taluk, division> {

		public TalukController()
			: this(new TalukRepository()) {
		}

		public TalukController(ITalukRepository currencyRepository) {
			_TalukRepository = currencyRepository;
		}

		ITalukRepository _TalukRepository;

		[HttpGet]
		[ActionName("Select")]
		public List<AutoCompleteList> GetTaluks([FromUri] string term, [FromUri] int pageSize = 10) {
			return _TalukRepository.GetTaluks(term, pageSize);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Post(Taluk contract) {
			return base.Post(contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Put(int id, Taluk contract) {
			return base.Put(id, contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Delete(int id) {
			return base.Delete(id);
		}
	}
	 
}