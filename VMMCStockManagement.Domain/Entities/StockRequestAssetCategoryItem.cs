using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Entities
{
	public class StockRequestAssetCategoryItem : BaseEntity
	{
		public long StockId { get; set; }
		public Stock Stock { get; set; }
		public long StockRequestAssetCategoryId { get; set; }

		public static StockRequestAssetCategoryItem Create(long stockId, long stockRequestAssetCategoryId, string userId)
		{
			return new StockRequestAssetCategoryItem
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = userId,
				UpdatedBy = userId,
				StockId = stockId,
				StockRequestAssetCategoryId = stockRequestAssetCategoryId,
			};
		}
	}
}
