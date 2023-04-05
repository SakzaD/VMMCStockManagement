using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse
{
	public class ReportListResponse
	{
		public string RequestedDate { get; set; }
		public string DateExpected { get; set; }
		public string DateAssigned { get; set; }
		public string AssetType { get; set; }
		public string Department { get; set; }
		public string Location { get; set; }
		public string AssinedTo { get; set; }
	}
}
