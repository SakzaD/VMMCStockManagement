using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.Stock
{
	public class StockResponse
	{
		public long Id { get; set; }
		public string DateCaptured { get; set; }		
		public string StockId { get; set; }		
		public string Capturer { get; set; }		
		public string StockStatus { get; set; }
	}
}
