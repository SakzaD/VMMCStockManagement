using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Requests
{
	public class UserAssetRequest : BaseRequest
	{
		public long? AssetRequestId { get; set; }
		public bool IsEmployee { get; set; }
		public string? EmployeeNumber { get; set; }
		public string? AssigneeUserId { get; set; }
		public string? IdNumber { get; set; }
		public long? AssetId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Reason { get; set; }
		public string? ReferenceNumber { get; set; }


	}
}
