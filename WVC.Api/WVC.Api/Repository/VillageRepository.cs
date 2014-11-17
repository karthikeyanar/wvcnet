using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface IVillageRepository {
		List<AutoCompleteList> GetVillages(string name, int pageSize = 1000);
	}

	public class VillageRepository : IVillageRepository {

		public List<AutoCompleteList> GetVillages(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
                IQueryable<wvc_village> ecVillages = context.wvc_village;
				if (string.IsNullOrEmpty(name) == false) {
					ecVillages = (from village in ecVillages
								   where village.name.StartsWith(name)  
								   select village);
				}
				IQueryable<AutoCompleteList> query = (from village in ecVillages
													  orderby village.name
													  select new AutoCompleteList {
														  id = village.id,
														  label = village.name,
														  value = village.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}