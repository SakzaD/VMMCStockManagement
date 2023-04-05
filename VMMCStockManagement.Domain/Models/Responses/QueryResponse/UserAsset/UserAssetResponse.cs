using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse.UserAsset
{
	public class UserAssetResponse
	{
		public string SerialNumber;

		public long Id { get; set; }
		public string UserId { get; set; }
		public long AssetId { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public string AssetHolder { get; set; }
		public string DateAssigned { get; set; }
		public string RegistrationNumber { get; set; }
		public string Category { get; set; }
		public string AssetDescription { get; set; }
	}
}
