using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface IDivisionRepository {
		List<AutoCompleteList> GetDivisions(string name, int pageSize = 1000);
	}

	public class DivisionRepository : IDivisionRepository {

		public List<AutoCompleteList> GetDivisions(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
				IQueryable<division> ecDivisions = context.divisions;
				if (string.IsNullOrEmpty(name) == false) {
					ecDivisions = (from division in ecDivisions
								   where division.name.StartsWith(name)  
								   select division);
				}
				IQueryable<AutoCompleteList> query = (from division in ecDivisions
													  orderby division.name
													  select new AutoCompleteList {
														  id = division.id,
														  label = division.name,
														  value = division.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}