using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models.Responses.QueryResponse
{
	public class SearchedAssetListResponse
	{
		public long Id { get; set; }
		public bool IsReturned { get; set; }
		public string AssetHolder { get; set; }
		public string EmployeeNumber { get; set; }
		public string DateAssigned { get; set; }
		public string Status { get; set; }
		public string StatusDescription { get; set; }
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public string Category { get; set; }
	}
}
