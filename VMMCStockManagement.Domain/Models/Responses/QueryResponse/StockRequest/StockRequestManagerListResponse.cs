using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest
{
	public class StockRequestManagerListResponse
	{
		public long Id { get; set; }
		public string DateRequested { get; set; }
		public string ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public string Requester { get; set; }
		public string EmployeeNumber { get; set; }
		public int NumberOfAssets { get; set; }
	}
}
