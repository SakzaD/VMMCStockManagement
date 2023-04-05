using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class StockRequestAssetCategory : BaseEntity
	{
		public long? StockRequestId { get; set; }
		public long? StockCategoryId { get; set; }
		public int Qty { get; set; }
		public string? FileName { get; set; }
		public string? HardwareSpecification { get; set; }
		public Category Category { get; set; }
		public List<StockRequestAssetCategoryItem> StockRequestAssetCategoryItems { get; set; }

		public static StockRequestAssetCategory Create(StockRequestAssetCategoryRequest request)
		{
			var assets = new List<StockRequestAssetCategoryItem>();

			if (request.StocksIds != null && request.StocksIds.Any())
			{


				foreach (var itemId in request.StocksIds)
				{

					assets.Add(new StockRequestAssetCategoryItem
					{
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
						CreatedBy = request.UserId,
						UpdatedBy = request.UserId,
						StockId = itemId,

					});
				}
			}

			return new StockRequestAssetCategory
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				HardwareSpecification = request.HardwareSpecification,
				StockCategoryId = request.StockCategoryId,
				Qty = request.Qty.Value,
				FileName = request.FileName
			};
		}


	}
}
