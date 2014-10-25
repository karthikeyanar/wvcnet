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

	[RoutePrefix("Village")]
	public class VillageController : BaseApiController<Village, division> {

		public VillageController()
			: this(new VillageRepository()) {
		}

		public VillageController(IVillageRepository currencyRepository) {
			_VillageRepository = currencyRepository;
		}

		IVillageRepository _VillageRepository;

		[HttpGet]
		[ActionName("Select")]
		public List<AutoCompleteList> GetVillages([FromUri] string term, [FromUri] int pageSize = 10) {
			return _VillageRepository.GetVillages(term, pageSize);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Post(Village contract) {
			return base.Post(contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Put(int id, Village contract) {
			return base.Put(id, contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Delete(int id) {
			return base.Delete(id);
		}
	}
	 
}