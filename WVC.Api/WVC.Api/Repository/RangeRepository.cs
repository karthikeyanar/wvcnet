using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface IRangeRepository {
		List<AutoCompleteList> GetRanges(string name, int pageSize = 1000);
	}

	public class RangeRepository : IRangeRepository {

		public List<AutoCompleteList> GetRanges(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
                IQueryable<wvc_range> ecRanges = context.wvc_range;
				if (string.IsNullOrEmpty(name) == false) {
					ecRanges = (from range in ecRanges
								   where range.name.StartsWith(name)  
								   select range);
				}
				IQueryable<AutoCompleteList> query = (from range in ecRanges
													  orderby range.name
													  select new AutoCompleteList {
														  id = range.id,
														  label = range.name,
														  value = range.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}