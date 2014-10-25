﻿using WVC.Api.Repository;
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

	[RoutePrefix("District")]
	public class DistrictController : BaseApiController<District, division> {

		public DistrictController()
			: this(new DistrictRepository()) {
		}

		public DistrictController(IDistrictRepository currencyRepository) {
			_DistrictRepository = currencyRepository;
		}

		IDistrictRepository _DistrictRepository;

		[HttpGet]
		[ActionName("Select")]
		public List<AutoCompleteList> GetDistricts([FromUri] string term, [FromUri] int pageSize = 10) {
			return _DistrictRepository.GetDistricts(term, pageSize);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Post(District contract) {
			return base.Post(contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Put(int id, District contract) {
			return base.Put(id, contract);
		}

		[Authorize(Roles = "Admin")]
		public override IHttpActionResult Delete(int id) {
			return base.Delete(id);
		}
	}
	 
}