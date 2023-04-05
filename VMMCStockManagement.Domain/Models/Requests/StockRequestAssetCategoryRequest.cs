using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class StockRequestAssetCategoryRequest : BaseRequest
	{
		public long StockRequestId { get; set; }
		public string? HardwareSpecification { get; set; }
		public long StockCategoryId { get; set; }
		public List<long> StocksIds { get; set; }
		public int? Qty { get; set; } = 0;
		public string? FileName { get; set; }
	}
}
