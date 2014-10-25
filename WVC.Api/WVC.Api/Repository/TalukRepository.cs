using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface ITalukRepository {
		List<AutoCompleteList> GetTaluks(string name, int pageSize = 1000);
	}

	public class TalukRepository : ITalukRepository {

		public List<AutoCompleteList> GetTaluks(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
				IQueryable<taluk> ecTaluks = context.taluks;
				if (string.IsNullOrEmpty(name) == false) {
					ecTaluks = (from taluk in ecTaluks
								   where taluk.name.StartsWith(name)  
								   select taluk);
				}
				IQueryable<AutoCompleteList> query = (from taluk in ecTaluks
													  orderby taluk.name
													  select new AutoCompleteList {
														  id = taluk.id,
														  label = taluk.name,
														  value = taluk.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}