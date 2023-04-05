using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest
{
	public class StockRequestManagerResponse
	{
		public string Requester { get; set; }
		public long GrantId { get; set; }
		public string EmployeeNumber { get; set; }
		public string Grant { get; set; }
		public string ReferenceNumber { get; set; }
		public string TicketNumber { get; set; }
		public int NumberOutstanding { get { return RequestedItems == null ? 0 : RequestedItems.Sum(x => x.RemainingQty); } }
		public List<RequestedItem> RequestedItems { get; set; } = new List<RequestedItem>();
	}

	public class RequestedItem
	{
		//public long Id { get; set; }
		public long CategoryId { get; set; }
		public string Description { get; set; }
		public int RequestedQty { get; set; }
		public int AssignedQty { get; set; }
		public int RemainingQty
		{
			get
			{

				int total = (RequestedQty - AssignedQty);

				if (total < 0)
					total = 0;
				return total;

			}
		}
	}
}
