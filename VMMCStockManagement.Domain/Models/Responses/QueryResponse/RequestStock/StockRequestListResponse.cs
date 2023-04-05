using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.RequestStock
{
	public class StockRequestListResponse
	{
		public long Id { get; set; }
		public string Status { get; set; }
		public bool CanAction { get; set; }
		public string StatusDescription { get; set; }
		public string DateCreated { get; set; }
		public string TicketNumber { get; set; }
		public string ReferenceNumber { get; set; }
		public string JobTitle { get; set; }
		public string Grant { get; set; }
		public string Department { get; set; }
		public string Email { get; set; }
		public string ManagerName { get; set; }
		public string Phone { get; set; }
		public string DateRequired { get; set; }
		public string Reason { get; set; }
		public string Facility { get; set; }
		public string OtherReason { get; set; }
		public List<RequestedStockResponse> RequestedStockResponses { get; set; } = new List<RequestedStockResponse>();
	}
}
