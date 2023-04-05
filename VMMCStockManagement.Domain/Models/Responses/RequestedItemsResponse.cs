using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses
{
	public class RequestedItemsResponse
	{
		public string Name { get; set; }
		public int RequestedQty { get; set; }
		public int AssignedQty { get; set; }
		public int RemainingQty { get; set; }
	}
}
