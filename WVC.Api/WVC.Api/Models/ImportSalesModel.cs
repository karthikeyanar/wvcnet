using WVC.Framework;
using WVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace WVC.Api.Models {

	public class ImportSalesModel : ImportPaging {

		[DisplayName("Company Code")]
		public string CompanyCode { get; set; }

		[DisplayName("Airline Code")]
		public string AirlineCode { get; set; }

		[DisplayName("GSA Name")]
		public string GSAName { get; set; }

		[DisplayName("GSA Airport")]
		public string GSAAirport { get; set; }

		[DisplayName("AWB No")]
		public string AWBNO { get; set; }

		[DisplayName("Flight No")]
		public string FlightNO { get; set; }

		[DisplayName("Flight Date")]
		public string FlightDate { get; set; }

		[DisplayName("Origin")]
		public string Origin { get; set; }

		[DisplayName("Destination")]
		public string Destination { get; set; }

		[DisplayName("Commodity")]
		public string Commodity { get; set; }

		[DisplayName("PP/CC")]
		public string PPCC { get; set; }

		[DisplayName("Gr.Wt")]
		public string GRWT { get; set; }

		[DisplayName("Ch.Wt")]
		public string CHWT { get; set; }

		[DisplayName("Currency")]
		public string Currency { get; set; }

		[DisplayName("Freight/KG")]
		public string FreightKG { get; set; }

		[DisplayName("Freight Amount")]
		public string FreightAmount { get; set; }

		[DisplayName("FSC Amount")]
		public string FSCAmount { get; set; }

		[DisplayName("SSC Amount")]
		public string SSCAmount { get; set; }

		[DisplayName("AWC")]
		public string AWC { get; set; }

		[DisplayName("Screening")]
		public string Screening { get; set; }

		[DisplayName("Others")]
		public string Others { get; set; }

		[DisplayName("Total")]
		public string Total { get; set; }

		[DisplayName("IATA Commission")]
		public string IATACommission { get; set; }

		[DisplayName("Other Deduction")]
		public string OtherDeduction { get; set; }

		[DisplayName("Commission")]
		public string Commission { get; set; }

		[DisplayName("Due Carrier")]
		public string DueCarrier { get; set; }

		[DisplayName("Cost Freight")]
		public string CostFreight { get; set; }

		[DisplayName("Cost Interline")]
		public string CostInterline { get; set; }

		[DisplayName("Cost Surcharges")]
		public string CostSurcharges { get; set; }

		[DisplayName("Cost Trucking")]
		public string CostTrucking { get; set; }

		[DisplayName("Cost Handline")]
		public string CostHndling { get; set; }

		[DisplayName("Cost Others")]
		public string CostOthers { get; set; }

		[DisplayName("Total Cost")]
		public string TotalCost { get; set; }

		[DisplayName("Profit")]
		public string Profit { get; set; }

		[DisplayName("Profit Per KG")]
		public string ProfitPerKG { get; set; }
	}


	public class ImportPaging {

		[Range((int)0, int.MaxValue, ErrorMessage = "PageIndex is required")]
		[Required(ErrorMessage = "PageIndex is required")]
		public int PageIndex { get; set; }

		[Range((int)1, int.MaxValue, ErrorMessage = "PageIndex is required")]
		[Required(ErrorMessage = "PageIndex is required")]
		public int PageSize { get; set; }

		[Required(ErrorMessage = "Session Key is required")]
		public string SessionKey { get; set; }

	}

	public class ImportResultModel {

		public int? TotalRows { get; set; }

		public int? SuccessCount { get; set; }

		public int? ErrorCount { get; set; }

		public bool IsSuccess {
			get {
				return ((this.ErrorCount ?? 0) == 0);
			}
		}
	}
}