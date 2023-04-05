using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Models
{
	public class BulkStockModel
	{
		public int RowNumber { get; set; }
		public string? Make { get; set; }
		public string? Model { get; set; }
		public string? RegisteredNumber { get; set; }
		public string? Description { get; set; }
		public string? SerialNumber { get; set; }
		public string? UserName { get; set; }
		public string? Grant { get; set; }
		public string? Department { get; set; }
		public string? Location { get; set; }
		public string? PurchaseOrReceivedDate { get; set; }
		public string? PurchasePrice { get; set; }
		public string? SupplierName { get; set; }
		public string? WarrantyType { get; set; }
		public string? TransferDate { get; set; }
		public string? ReferenceNumber { get; set; }
	}
}
