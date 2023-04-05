using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequest
{
	public class RequestedStockResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public int Qty { get; set; }
		public int AssignedQty { get; set; }
		public int RemainingQty
		{
			get
			{
				int total = Qty - AssignedQty;
				return total < 0 ? 0 : total;
			}
		}
		public string HardwareSpecification { get; set; }
		public string? FileName { get; set; }
	}
}
