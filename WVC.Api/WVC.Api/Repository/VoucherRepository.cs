using WVC.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using WVC.Contracts;
using WVC.Models;

namespace WVC.Api.Repository {
	public interface IVoucherRepository {
		List<AutoCompleteList> GetVouchers(string name, int pageSize = 1000);
	}

	public class VoucherRepository : IVoucherRepository {

		public List<AutoCompleteList> GetVouchers(string name, int pageSize = 1000) {
			using (WVCContext context = new WVCContext()) {
                IQueryable<wvc_voucher> ecVouchers = context.wvc_voucher;
				if (string.IsNullOrEmpty(name) == false) {
					ecVouchers = (from voucher in ecVouchers
								   where voucher.name.StartsWith(name)  
								   select voucher);
				}
				IQueryable<AutoCompleteList> query = (from voucher in ecVouchers
													  orderby voucher.name
													  select new AutoCompleteList {
														  id = voucher.id,
														  label = voucher.name,
														  value = voucher.name
													  });
				return new PaginatedList<AutoCompleteList>(query, 1, pageSize);
			}
		}
		 
	}
}