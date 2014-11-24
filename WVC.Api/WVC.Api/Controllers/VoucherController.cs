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
    [RoutePrefix("Voucher")]
    public class VoucherController:BaseApiController<Voucher,wvc_voucher> {

        public VoucherController()
            : this(new VoucherRepository()) {
        }

        public VoucherController(IVoucherRepository currencyRepository) {
            _VoucherRepository = currencyRepository;
        }

        IVoucherRepository _VoucherRepository;

        [HttpGet]
        [ActionName("Select")]
        public List<AutoCompleteList> GetVouchers([FromUri] string term,[FromUri] int pageSize = 10) {
            return _VoucherRepository.GetVouchers(term,pageSize);
        }

        [Authorize(Roles = "member")]
        public override IHttpActionResult Post(Voucher contract) {
            return base.Post(contract);
        }

        [Authorize(Roles = "member")]
        public override IHttpActionResult Put(int id,Voucher contract) {
            return base.Put(id,contract);
        }

        [Authorize(Roles = "member")]
        public override IHttpActionResult Delete(int id) {
            return base.Delete(id);
        }
    }
}