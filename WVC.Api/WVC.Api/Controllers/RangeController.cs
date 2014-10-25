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

	[RoutePrefix("Range")]
	public class RangeController : BaseApiController<Range, division> {

		public RangeController()
			: this(new RangeRepository()) {
		}

		public RangeController(IRangeRepository currencyRepository) {
			_RangeRepository = currencyRepository;
		}

		IRangeRepository _RangeRepository;

		[HttpGet]
		[ActionName("Select")]
		public List<AutoCompleteList> GetRanges([FromUri] string term, [FromUri] int pageSize = 10) {
			return _RangeRepository.GetRanges(term, pageSize);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Post(Range contract) {
			return base.Post(contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Put(int id, Range contract) {
			return base.Put(id, contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Delete(int id) {
			return base.Delete(id);
		}
	}
	 
}