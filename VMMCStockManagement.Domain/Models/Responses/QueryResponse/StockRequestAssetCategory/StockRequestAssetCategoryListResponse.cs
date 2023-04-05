using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.StockRequestAssetCategory
{
	public class StockRequestAssetCategoryListResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Qty { get; set; }
		public string Message { get; set; } = string.Empty;
		public List<object> AddedItems { get; set; } = new List<object>();
	}
}
