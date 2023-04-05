using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class StockByReference : BaseEntity
	{
		public Stock? Stock { get; set; }
		public long AssetId { get; set; }
		public string ReferenceNumber { get; set; }


		//public UserAsset? UserAsset { get; set; }


		public static StockByReference Create(StockByReferenceRequest request)
		{
			return new StockByReference
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				AssetId = request.AssetId,
				ReferenceNumber = request.ReferenceNumber,
			};
		}
	}
}
