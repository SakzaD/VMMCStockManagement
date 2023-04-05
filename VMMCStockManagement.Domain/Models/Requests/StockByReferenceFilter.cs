using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class StockByReferenceRequest : BaseRequest
	{
		public long AssetId { get; set; }
		public string ReferenceNumber { get; set; }
	}
}
