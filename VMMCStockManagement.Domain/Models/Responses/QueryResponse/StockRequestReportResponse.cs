using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse
{
	public class StockRequestReportResponse
	{
		public long? Id { get; set; }
		public string? RequesterId { get; set; }
		public string RequesterName { get; set; }
		public long DepartmentId { get; set; }
		public string DepartmentName { get; set; }
		public long GrantId { get; set; }
		public string GrantName { get; set; }
		public bool Status { get; set; }
		public string DateRequested { get; set; }
		public string DateIssued { get; set; }
		public string? DateRequired { get; set; }

	}
}
