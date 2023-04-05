using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests.Filters
{
	public class StockRequestAssetCategoryFilter : BaseFilter
	{
		public long? StockRequestId { get; set; }
		public long? StockCategoryId { get; set; }
	}
}
