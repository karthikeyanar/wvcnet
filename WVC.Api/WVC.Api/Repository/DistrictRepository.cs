using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface IDistrictRepository {
		List<AutoCompleteList> GetDistricts(string name, int pageSize = 1000);
	}

	public class DistrictRepository : IDistrictRepository {

		public List<AutoCompleteList> GetDistricts(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
				IQueryable<district> ecDistricts = context.districts;
				if (string.IsNullOrEmpty(name) == false) {
					ecDistricts = (from district in ecDistricts
								   where district.name.StartsWith(name)  
								   select district);
				}
				IQueryable<AutoCompleteList> query = (from district in ecDistricts
													  orderby district.name
													  select new AutoCompleteList {
														  id = district.id,
														  label = district.name,
														  value = district.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}