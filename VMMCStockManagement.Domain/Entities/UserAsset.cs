using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Models.Requests;

namespace VMMCStockManagement.Domain.Entities
{
	public class UserAsset : BaseEntity
	{
		public string UserId { get; set; }
		public User? User { get; set; }
		public long? AssetId { get; set; }
		public Stock? Stock { get; set; }
		public string? Reason { get; set; }
		public string? ReferenceNumber { get; set; }
		public bool IsReturned { get; set; } = false;

		public static UserAsset Create(UserAssetRequest request)
		{
			return new UserAsset
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				CreatedBy = request.UserId,
				UpdatedBy = request.UserId,
				AssetId = request.AssetId,
				UserId = request.AssigneeUserId,
				Reason = request.Reason,
				ReferenceNumber = request.ReferenceNumber,

			};
		}
	}
}
